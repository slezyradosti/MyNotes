import { observer } from "mobx-react-lite";
import { Button, Grid, Input, Item, TextArea } from "semantic-ui-react";
import { SyntheticEvent, useEffect, useRef, useState } from "react";
import Note from "../../../app/models/note";

interface Props {
    noteArray: Note[];
    noteLoading: boolean;
    noteEditMode: boolean;
    noteOpenForm: (id?: string | undefined) => void;
    noteSelect: (id: string) => void;
    noteUpdate: (note: Note) => Promise<void>;
    noteDelete: (id: string) => Promise<void>;
    getNote: (id: string) => Note;
}

function NoteList({ noteArray, noteLoading, noteEditMode,
    noteOpenForm, noteSelect, noteUpdate, noteDelete, getNote }: Props) {
    const inputRef = useRef<Input | TextArea | null>(null);
    // Local state for editing a note
    const [editNoteId, setEditNoteId] = useState<string | null>(null);
    const [editedNote, setEditedNote] = useState<Note | null>(null);
    const [target, setTarget] = useState('');

    // changes focus to the editing name
    useEffect(() => {
        if (inputRef.current) {
            inputRef.current.focus();
        }
    }, [editNoteId]);

    // ... (other functions and handlers)

    // Handler for editing a note
    const handleEditNoteStart = (noteId: string) => {
        const noteToEdit = noteArray.find((note) => note.id === noteId);
        if (noteToEdit) {
            setEditNoteId(noteId);
            setEditedNote({ ...noteToEdit });
        }
    };

    // Handler for saving an edited note
    const handleSaveNote = (noteId: string) => {
        // Implement saving the edited note, e.g., make an API call
        // Update the noteArray with the edited note
        let newNote = getNote(noteId);
        newNote.name = editedNote?.name;
        newNote.record = editedNote?.record || '';
        //TODO 
        //change newNote to editedNote
        noteUpdate(newNote);

        // Clear the edit mode
        setEditNoteId(null);
        setEditedNote(null);
    };

    // Handler for canceling editing a note
    const handleEditNoteCancel = () => {
        setEditNoteId(null);
        setEditedNote(null);
    };

    // Handler for creating a new note
    const handleCreateNote = () => {
        // Implement creating a new note, e.g., make an API call
        // Append the new note to the noteArray
        // Clear the input fields
        setEditNoteId(null);
        setEditedNote(null);
    };

    // Handler for deleting a note
    const handleDeleteNote = (e: SyntheticEvent<HTMLButtonElement>, noteId: string) => {
        // Implement deleting the note, e.g., make an API call
        // Remove the note from the noteArray
        setTarget(noteId);
        noteDelete(noteId);
    };

    // Handler for editing changes in the note
    const handleEditChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
        field: keyof Note) => {
        if (editedNote) {
            setEditedNote({ ...editedNote, [field]: e.target.value });
        }
    };

    // ... (other handlers for creating and deleting notes)

    return (
        <Item.Group divided>
            <Grid columns={3} doubling stackable>
                {noteArray.map((note) => (
                    <Grid.Column key={note.id}>
                        <Item key={note.id}>
                            <Item.Content>
                                {editNoteId === note.id ? (
                                    <div>
                                        <Input
                                            ref={(input) => (inputRef.current = input)}
                                            type="text"
                                            value={editedNote?.name}
                                            onChange={(e) => handleEditChange(e, "name")}
                                        />
                                        <TextArea
                                            ref={(input) => (inputRef.current = input)}
                                            value={editedNote?.record}
                                            onChange={(e) => handleEditChange(e, "record")}
                                        />
                                        <br />
                                        <Button
                                            onClick={() => handleSaveNote(note.id!)}
                                            color='green'
                                        >
                                            Save
                                        </Button>
                                        <Button
                                            onClick={handleEditNoteCancel}
                                            color='red'
                                        >
                                            Cancel
                                        </Button>
                                    </div>
                                ) : (
                                    <div>
                                        {/* Use a label as a clickable element */}
                                        <label
                                            onClick={() => handleEditNoteStart(note.id!)}
                                            style={{ cursor: "pointer" }}
                                        >
                                            {note.name}
                                        </label>
                                        <Button
                                            loading={noteLoading && target === note.id}
                                            onClick={(e) => handleDeleteNote(e, note.id!)}
                                        >
                                            x
                                        </Button>
                                        <br />
                                        {/* Use a div for displaying the description */}
                                        <Item.Meta>{note.createdAt}</Item.Meta>
                                        <div onClick={() => handleEditNoteCancel(note.id!)}>{note.record}</div>
                                    </div>
                                )}
                            </Item.Content>
                        </Item>
                    </Grid.Column>
                ))}
            </Grid>
        </Item.Group>
    );
}

export default observer(NoteList);