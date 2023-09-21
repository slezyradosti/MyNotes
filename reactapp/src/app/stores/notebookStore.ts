import { makeAutoObservable } from "mobx";
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
}

export default NotebookStore;