import { observer } from "mobx-react-lite";
import { Button, Divider, Grid, Input, Item, TextArea, SemanticWIDTHS, Loader } from "semantic-ui-react";
import { SyntheticEvent, useEffect, useRef, useState } from "react";
import Note from "../../../app/models/note";
import NoteForm from "../form/NoteForm";
import HelpButton from "../other/HelpButton";
import NoteListContent from "./NoteListContent";
import MyDropZone from "../../../app/common/modals/imageUpload/PhotoWidgetDropzone";
import { useStore } from "../../../app/stores/store";

interface Props {
    noteArray: Note[];
    noteLoading: boolean;
    noteEditMode: boolean;
    noteSelectedElement: Note | undefined;
    noteOpenForm: (id?: string | undefined) => void;
    noteSelect: (id: string) => void;
    noteUpdate: (note: Note) => Promise<void>;
    noteDelete: (id: string) => Promise<void>;
    getNote: (id: string) => Note;
    cancelSelectedNote: () => void;
    columnsCount: SemanticWIDTHS | "equal" | undefined;
    loadingNext: boolean;
}

function NoteList({ noteArray, noteLoading, noteEditMode, noteSelectedElement,
    noteSelect, noteUpdate, noteDelete, getNote, cancelSelectedNote, columnsCount,
    loadingNext }: Props) {

    const { photoStore } = useStore();
    const inputRef = useRef<Input | TextArea | null>(null);
    // Local state for editing a note
    const [editNoteId, setEditNoteId] = useState<string | null>(null);
    const [editedNote, setEditedNote] = useState<Note | null>(null);
    const [rowsCount, setrowsCount] = useState<number | undefined>(5);

    // changes focus to the editing name
    useEffect(() => {
        if (inputRef.current) {
            inputRef.current.focus();
        }
    }, [editNoteId]);
    // Handler for editing a note
    const handleEditNoteStart = (noteId: string) => {
        const noteToEdit = noteArray.find((note) => note.id === noteId);
        if (noteToEdit) {
            setEditNoteId(noteId);
            setEditedNote({ ...noteToEdit });
            noteSelect(noteId);
        }
    };

    // Handler for saving an edited note
    const handleSaveNote = async (noteId: string) => {
        // Implement saving the edited note, e.g., make an API call
        // Update the noteArray with the edited note
        let newNote = getNote(noteId);
        newNote.name = editedNote?.name;
        newNote.record = editedNote?.record || '';

        await noteUpdate(newNote);

        // Clear the edit mode
        setEditNoteId(null);
        setEditedNote(null);
        cancelSelectedNote();
    };

    // Handler for canceling editing a note
    const handleEditNoteCancel = () => {
        setEditNoteId(null);
        setEditedNote(null);
        cancelSelectedNote();
    };

    // Handler for deleting a note
    const handleDeleteNote = (e: SyntheticEvent<HTMLButtonElement>, noteId: string) => {
        // Implement deleting the note, e.g., make an API call
        // Remove the note from the noteArray
        noteSelect(noteId);
        noteDelete(noteId);
    };

    // Handler for editing changes in the note
    const handleEditChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
        field: keyof Note) => {
        if (editedNote) {
            setEditedNote({ ...editedNote, [field]: e.target.value });
        }
    };

    const handleUploadPhoto = (file: Blob) => {
        //save photo
        photoStore.uploadPhoto(file, noteSelectedElement!.id!);
    }

    const handleUploadPhotoToRecord = async () => {
        //add link for the photo to the record
        let newNote = getNote(editedNote!.id!);
        newNote.record += `\n![Image](${photoStore.selectedElement?.url})`
        await noteUpdate(newNote);

        handleEditNoteCancel();
    }

    return (
        <>
            <Item.Group divided>
                <Grid columns={columnsCount} doubling stackable>
                    {noteArray.map((note) => (
                        <Grid.Column key={note.id}>
                            <Item key={note.id}>
                                <Item.Content>
                                    {editNoteId === note.id ? (
                                        <div className="ui form">
                                            <div className="field">
                                                <Input
                                                    required={true}
                                                    ref={(input) => (inputRef.current = input)}
                                                    value={editedNote?.name}
                                                    name='name'
                                                    onChange={(e) => handleEditChange(e, "name")}
                                                    fluid
                                                />
                                            </div>
                                            <div className="field">
                                                <TextArea
                                                    required={true}
                                                    rows={rowsCount}
                                                    ref={(input) => (inputRef.current = input)}
                                                    value={editedNote?.record}
                                                    name='record'
                                                    onChange={(e) => handleEditChange(e, "record")}
                                                    fluid
                                                />
                                            </div>
                                            <div style={{ display: 'flex', justifyContent: 'end', alignItems: 'center' }}>
                                                <HelpButton />
                                                <MyDropZone
                                                    uploadPhoto={handleUploadPhoto}
                                                    uploadPhotoToRecord={handleUploadPhotoToRecord}
                                                    loading={photoStore.loading}
                                                />
                                                <Button
                                                    onClick={handleEditNoteCancel}
                                                    className="cancelBtnColor Border"
                                                    content='Cancel'
                                                />

                                                <Button
                                                    onClick={() => handleSaveNote(note.id!)}
                                                    loading={noteLoading}
                                                    className="submitBtnColor Border"
                                                    content='Save'
                                                />
                                            </div>

                                        </div>
                                    ) : (
                                        <NoteListContent
                                            note={note}
                                            noteLoading={noteLoading}
                                            noteSelectedElement={noteSelectedElement}
                                            noteEditMode={noteEditMode}
                                            handleEditNoteStart={handleEditNoteStart}
                                            handleDeleteNote={handleDeleteNote}
                                        />
                                    )}
                                </Item.Content>
                            </Item>
                            <Divider />
                        </Grid.Column>
                    ))}
                    <Grid.Column>
                        <Loader active={loadingNext} />
                    </Grid.Column>

                    {noteEditMode &&
                        <Grid.Column key={'newNoteId'}>
                            <Item key={'newNoteId'}>
                                <Item.Content>
                                    <NoteForm />
                                </Item.Content>
                            </Item>
                        </Grid.Column>
                    }
                </Grid>
            </Item.Group >
        </>
    );
}

export default observer(NoteList);