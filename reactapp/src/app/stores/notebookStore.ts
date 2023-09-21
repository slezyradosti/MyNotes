import { makeAutoObservable, runInAction } from "mobx";
import { Notebook } from "../models/notebook";
import agent from "../api/agent";

class NotebookStore {
    notebookRegistry = new Map<string, Notebook>();
    selectedNotebook: Notebook | undefined = undefined;
    editMode = false;
    loading = false;
    laoadingInital = true;

    constructor() {
        makeAutoObservable(this)
    }

    get notebooksArray() {
        return Array.from(this.notebookRegistry.values());
    }

    loadNotebooks = async () => {

        try {
            const notebooks = await agent.Notebooks.list();
            notebooks.forEach(notebook => {
                notebook.createdAt = notebook.createdAt?.split('T')[0];
                this.notebookRegistry.set(notebook.id!, notebook);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    setLoadingInitial = (state: boolean) => {
        this.laoadingInital = state;
    }

    selectNotebook = (id: string) => {
        this.selectedNotebook = this.notebookRegistry.get(id);
    }

    cancelSelectedNotebook = () => {
        this.selectedNotebook = undefined;
    }

    openForm = (id?: string) => {
        id ? this.selectNotebook(id) : this.cancelSelectedNotebook();
        this.editMode = true;
    }

    closeForm = () => {
        this.editMode = false;
    }

    createNotebook = async (notebook: Notebook) => {
        this.loading = true;

        try {
            const newNotebook = await agent.Notebooks.create(notebook);
            const da = await agent.Notebooks.create(notebook).then(data => data.id);
            runInAction(() => {
                this.notebookRegistry.set(notebook.id!, notebook);
                this.selectedNotebook = newNotebook;
                console.log('notebook: ' + notebook.id, notebook.createdAt, notebook.name);
                console.log('new notebook: ' + newNotebook.id, notebook.createdAt, notebook.name);
                console.log('da: ' + da);
                this.editMode = false;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    updateNotebook = async (notebook: Notebook) => {
        this.loading = true;

        try {
            await agent.Notebooks.update(notebook);
            runInAction(() => {
                this.notebookRegistry.set(notebook.id!, notebook);
                this.selectedNotebook = notebook;
                this.editMode = false;
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    deleteNotebook = async (id: string) => {
        this.loading = true;

        try {
            await agent.Notebooks.delete(id);
            runInAction(() => {
                this.notebookRegistry.delete(id);
                if (this.selectedNotebook?.id === id) this.cancelSelectedNotebook();
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }
}

export default NotebookStore;