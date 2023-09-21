import { makeAutoObservable, runInAction } from "mobx";
import { Notebook } from "../models/notebook";
import agent from "../api/agent";

class NotebookStore {
    notebooks: Notebook[] = [];
    selectedNotebook: Notebook | undefined = undefined;
    editMode = false;
    loading = false;
    laoadingInital = false;

    constructor() {
        makeAutoObservable(this)
    }

    loadNotebooks = async () => {
        this.setLoadingInitial(true);

        try {
            const notebooks = await agent.Notebooks.list();
            notebooks.forEach(notebook => {
                notebook.createdAt = notebook.createdAt?.split('T')[0];
                this.notebooks.push(notebook);
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
        this.selectedNotebook = this.notebooks.find(a => a.id === id);
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
            await agent.Notebooks.create(notebook);
            runInAction(() => {
                this.notebooks.push(notebook);
                this.selectedNotebook = notebook;
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
                this.notebooks = [...this.notebooks.filter(n => n.id !== notebook.id), notebook];
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
                this.notebooks = [...this.notebooks.filter(n => n.id !== id)];
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