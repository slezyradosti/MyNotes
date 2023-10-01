import { observer } from "mobx-react-lite";
import { Button, Divider, FormTextArea, Grid, Icon, Input, Item, Segment, TextArea, Form, Label } from "semantic-ui-react";
import { SyntheticEvent, useEffect, useRef, useState } from "react";
import Note from "../../../app/models/note";
import NoteForm from "../form/NoteForm";

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
}

function NoteList({ noteArray, noteLoading, noteEditMode, noteSelectedElement,
    noteSelect, noteUpdate, noteDelete, getNote }: Props) {
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

    return (
        <>
            <Item.Group divided>
                <Grid columns={2} doubling stackable>
                    {noteArray.map((note) => (
                        <Grid.Column key={note.id}>
                            <Item key={note.id}>
                                <Item.Content>
                                    {editNoteId === note.id ? (
                                        <div className="ui form">
                                            <Input
                                                ref={(input) => (inputRef.current = input)}

                                                value={editedNote?.name}
                                                name='name'
                                                onChange={(e) => handleEditChange(e, "name")}
                                                fluid
                                            />
                                            <br />
                                            <TextArea
                                                rows={rowsCount}
                                                ref={(input) => (inputRef.current = input)}
                                                value={editedNote?.record}
                                                name='record'
                                                onChange={(e) => handleEditChange(e, "record")}
                                                fluid
                                            />
                                            <br />
                                            <Button
                                                onClick={handleEditNoteCancel}
                                                className="cancelBtnColor Border"
                                            >
                                                Cancel
                                            </Button>
                                            <Button
                                                onClick={() => handleSaveNote(note.id!)}
                                                className="submitBtnColor Border"
                                            >
                                                Save
                                            </Button>
                                        </div>
                                    ) : (
                                        <div>
                                            {/* Use a label as a clickable element */}
                                            <div className="nameBtn">
                                                <label
                                                    onClick={() => handleEditNoteStart(note.id!)}
                                                    style={{ cursor: "pointer" }}
                                                >
                                                    {note.name}
                                                </label>
                                                <Button
                                                    loading={noteLoading && noteSelectedElement?.id === note.id && !noteEditMode}
                                                    onClick={(e) => handleDeleteNote(e, note.id!)}
                                                    style={{ backgroundColor: 'transparent' }}
                                                >
                                                    <Icon name='trash' />
                                                </Button>
                                            </div>
                                            {/* Use a div for displaying the description */}
                                            <Item.Meta style={{ color: '#808080' }}>{note.createdAt}</Item.Meta>
                                            <Label
                                                style={{ whiteSpace: 'pre-wrap', wordBreak: 'break-word' }}
                                                onClick={() => handleEditNoteStart(note.id!)}
                                                content={note.record}
                                            />
                                        </div>
                                    )}
                                </Item.Content>
                            </Item>
                            <Divider />
                        </Grid.Column>

                    ))}

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
            </Item.Group>
        </>
    );
}

export default observer(NoteList);