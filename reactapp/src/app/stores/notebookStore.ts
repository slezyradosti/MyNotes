import { makeAutoObservable, runInAction } from "mobx";
import { Notebook } from "../models/notebook";
import { v4 as uuid } from 'uuid';
import agent from "../api/agent";
import ISidebarListStore from "./ISidebarListStore";

class NotebookStore implements ISidebarListStore {
    notebookRegistry = new Map<string, Notebook>();
    selectedElement: Notebook | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    laoadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this)
    }

    get getArray() {
        return Array.from(this.notebookRegistry.values());
    }

    get getEntityType() {
        return 'Notebook';
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
        this.laoadingInitial = state;
    }

    selectOne = (id: string) => {
        this.selectedElement = this.notebookRegistry.get(id);
    }

    getOne = (id: string) => {
        return this.notebookRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.Notebooks.details(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    openForm = (id?: string) => {
        id ? this.selectOne(id) : this.cancelSelectedElement();
        this.editMode = true;
    }

    closeForm = () => {
        this.editMode = false;
    }

    createOne = async (notebook: Notebook) => {
        this.loading = true;
        notebook.id = uuid();

        try {
            await agent.Notebooks.create(notebook);
            notebook.createdAt = 'recently';
            runInAction(() => {
                this.notebookRegistry.set(notebook.id!, notebook);
                this.selectedElement = notebook;
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

    updateOne = async (notebook: Notebook) => {
        this.loading = true;
        notebook.createdAt = 'recently';

        try {
            await agent.Notebooks.update(notebook);
            runInAction(() => {
                this.notebookRegistry.set(notebook.id!, notebook);
                this.selectedElement = notebook;
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

    deleteOne = async (id: string) => {
        this.loading = true;

        try {
            await agent.Notebooks.delete(id);
            runInAction(() => {
                this.notebookRegistry.delete(id);
                if (this.selectedElement?.id === id) this.cancelSelectedElement();
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