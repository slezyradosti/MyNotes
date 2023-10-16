import { makeAutoObservable, runInAction } from "mobx";
import Note from "../models/note";
import { store } from "./store";

class NoteExtensionSotre {
    editNoteId: string | null = null;
    editedNote: Note | null = null;
    rowsCount: number | undefined = 5;


    constructor() {
        makeAutoObservable(this);
    }

    setEditNoteId = (noteId: string | null) => {
        this.editNoteId = noteId;
    }

    setEditedNote = (note: Note | null) => {
        this.editedNote = note;
    }

    setRowsCount = (rowsCount: number | undefined) => {
        this.rowsCount = rowsCount;
    }

    uploadPhoto = async (file: Blob) => {
        store.noteStore.setLoading(true);
        try {
            await store.photoStore.uploadPhoto(file, store.noteStore.selectedElement!.id!);
            this.handleUploadPhotoToRecord();
        } catch (error) {
            runInAction(() => store.noteStore.setLoading(false))
        }
        //save photo
    }

    private handleUploadPhotoToRecord = async () => {
        //add link for the photo to the record
        let newNote = store.noteStore.getOne(this.editedNote!.id!);
        newNote.record += `\n![Image](${store.photoStore.selectedElement?.url})`
        await store.noteStore.updateOne(newNote);
    }

    deletePhotoFromRecord = async (noteId: string, photoUrl: string) => {
        try {
            let newNote = store.noteStore.getOne(noteId);
            newNote.record = newNote.record.replace(`![Image](${photoUrl})`, '')
            await store.noteStore.updateOne(newNote);
        } catch (error) {
            console.log(error);
        }
    }
}

export default NoteExtensionSotre;