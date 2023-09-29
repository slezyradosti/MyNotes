import { makeAutoObservable, runInAction } from "mobx";
import Note from "../models/note";
import agent from "../api/agent";
import { v4 as uuid } from 'uuid';
import moment from "moment";

class NoteStore {
    noteRegistry = new Map<string, Note>();
    selectedElement: Note | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this)
    }

    get getArray() {
        return Array.from(this.noteRegistry.values());
    }

    get getEntityType() {
        return 'Note';
    }

    loadNotes = async (pageId: string) => {
        try {
            const notes = await agent.Notes.list(pageId);
            notes.forEach(note => {
                note.createdAt = note.createdAt?.split('T')[0];
                this.noteRegistry.set(note.id!, note);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
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
}

export default NoteStore;