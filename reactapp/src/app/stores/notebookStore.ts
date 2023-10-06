import { makeAutoObservable, runInAction } from "mobx";
import { Notebook } from "../models/notebook";
import { v4 as uuid } from 'uuid';
import agent from "../api/agent";
import ISidebarListStore from "./ISidebarListStore";
import moment from "moment";
import { Pagination, PagingParams } from "../models/pagination";

class NotebookStore implements ISidebarListStore {
    notebookRegistry = new Map<string, Notebook>();
    selectedElement: Notebook | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();


    constructor() {
        makeAutoObservable(this)
    }

    setPagingParams = (pagingParams: PagingParams) => {
        this.pagingParams = pagingParams;
    }

    get axiosParams() {
        const params = new URLSearchParams();
        params.append('PageIndex', this.pagingParams.paramPageIndex.toString());
        params.append('PageSize', this.pagingParams.paramPageSize.toString());
        return params;
    }

    get getArray() {
        return Array.from(this.notebookRegistry.values());
    }

    get getEntityType() {
        return 'Notebook';
    }

    loadData = async () => {

        try {
            const result = await agent.Notebooks.list(this.axiosParams);
            result.data.forEach(notebook => {
                notebook.createdAt = notebook.createdAt?.split('T')[0];
                this.notebookRegistry.set(notebook.id!, notebook);
            })
            this.setPagination(result.pagination!);
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
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
            notebook.createdAt = moment().format('YYYY-MM-DD');
            runInAction(() => {
                this.notebookRegistry.set(notebook.id!, notebook);
                this.selectedElement = notebook;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    updateOne = async (notebook: Notebook) => {
        this.loading = true;

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