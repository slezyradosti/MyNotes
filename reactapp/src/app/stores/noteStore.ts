import { makeAutoObservable, runInAction } from "mobx";
import Note from "../models/note";
import agent from "../api/agent";
import { v4 as uuid } from 'uuid';
import moment from "moment";
import { Pagination, PagingParams } from "../models/pagination";
import { store } from "./store";
import GraphCreationStatistic from "../models/graphCreationStatistic";

class NoteStore {
    noteRegistry = new Map<string, Note>();
    selectedElement: Note | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;
    columnsCount = localStorage.getItem('columnsCount') || "1";
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();
    loadingStatistic = true;
    creationStatisticArray: Array<GraphCreationStatistic> = [];

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
        return Array.from(this.noteRegistry.values());
    }

    get getEntityType() {
        return 'Note';
    }

    changeColumnsCount() {
        this.columnsCount === "1" ? this.columnsCount = "2" : this.columnsCount = "1";
        localStorage.setItem('columnsCount', this.columnsCount);
    }

    clearData = () => {
        this.noteRegistry.clear();
        this.setLoadingInitial(true);
    }

    loadNotes = async (pageId: string) => {
        if (this.pagingParams.paramPageIndex.toString() === '0') this.clearData();

        try {
            const result = await agent.Notes.list(pageId, this.axiosParams);
            result.data.forEach(note => {
                note.createdAt = note.createdAt?.split('T')[0];
                this.noteRegistry.set(note.id!, note);
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

    setLoading = (state: boolean) => {
        this.loading = state;
    }

    selectOne = (id: string) => {
        this.selectedElement = this.noteRegistry.get(id);
    }

    getOne = (id: string) => {
        return this.noteRegistry.get(id)!;
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

    createOne = async (note: Note) => {
        this.loading = true;
        note.id = uuid();

        try {
            await agent.Notes.create(note);
            note.createdAt = moment().format('YYYY-MM-DD');
            runInAction(() => {
                this.noteRegistry.set(note.id!, note);
                this.selectedElement = note;
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

    updateOne = async (note: Note) => {
        this.loading = true;

        try {
            await agent.Notes.update(note);
            runInAction(() => {
                this.noteRegistry.set(note.id!, note);
                this.selectedElement = note;
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
            store.photoStore.checkIfPhotosWereDeleted(note.id!, note.record);
        }
    }

    deleteOne = async (id: string) => {
        this.loading = true;

        try {
            await agent.Notes.delete(id);
            runInAction(() => {
                this.noteRegistry.delete(id);
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

    loadStatistic = async () => {
        try {
            const result = await agent.Notebooks.creationStatistic();
            result.forEach(item => {
                item.date = item.date?.split('T')[0];
                this.creationStatisticArray.push(item);
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.loadingStatistic = false);
        }
    }
}

export default NoteStore;