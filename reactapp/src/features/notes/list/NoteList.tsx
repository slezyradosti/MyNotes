import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { Button, Grid, Input, Item, TextArea } from "semantic-ui-react";
import { useEffect, useState } from "react";
import Note from "../../../app/models/note";

interface Props {
    noteArray: Note[];
}

function NoteList({ noteArray }: Props) {
    const { noteStore } = useStore();

    // Local state for editing a note
    const [editNoteId, setEditNoteId] = useState<string | null>(null);
    const [editedNote, setEditedNote] = useState<Note | null>(null);

    // ... (other functions and handlers)

    // Handler for editing a note
    const handleEditNote = (noteId: string) => {
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
        // Clear the edit mode
        setEditNoteId(null);
        setEditedNote(null);
    };

    // Handler for canceling editing a note
    const handleCancelEdit = () => {
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
    const handleDeleteNote = (noteId: string) => {
        // Implement deleting the note, e.g., make an API call
        // Remove the note from the noteArray
    };

    // Handler for editing changes in the note
    const handleEditChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
        field: keyof Note) => {
        if (editedNote) {
            setEditedNote({
                ...editedNote,
                [field]: e.target.value,
            });
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
                                            type="text"
                                            value={editedNote?.name}
                                            onChange={(e) => handleEditChange(e, "name")}
                                        />
                                        <TextArea
                                            type="text"
                                            value={editedNote?.record}
                                            onChange={(e) => handleEditChange(e, "record")}
                                        />
                                        <br />
                                        <Button onClick={() => handleSaveNote(note.id!)}>Save</Button>
                                        <Button onClick={handleCancelEdit}>Cancel</Button>
                                    </div>
                                ) : (
                                    <div>
                                        <Item.Header>{note.name}</Item.Header>
                                        <Item.Description>{note.record}</Item.Description>
                                        <Item.Meta>{note.createdAt}</Item.Meta>
                                        <Button onClick={() => handleEditNote(note.id!)}>Edit</Button>
                                        <Button onClick={() => handleDeleteNote(note.id!)}>Delete</Button>
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