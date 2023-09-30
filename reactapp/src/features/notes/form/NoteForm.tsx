import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { Button, Form } from "semantic-ui-react";
import { ChangeEvent, useState } from "react";

function NoteForm() {
    const { pageStore, noteStore } = useStore();
    const { loading, createOne, closeForm } = noteStore

    const initialState = {
        name: 'new note',
        record: 'new record',
        pageId: pageStore.selectedElement?.id ?? ''
    };

    const [noteDto, setNoteDto] = useState(initialState);

    function handleSubmit() {
        createOne(noteDto)
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target;
        setNoteDto({ ...noteDto, [name]: value });
    }

    return (
        <>
            <Form onSubmit={handleSubmit} autoComplete="off">
                <Form.Input
                    autoFocus
                    placeholder='Name'
                    value={noteDto.name}
                    name='name'
                    onChange={handleInputChange}
                    fluid
                />
                <Form.TextArea
                    autoFocus
                    placeholder='Record'
                    value={noteDto.record}
                    name='record'
                    onChange={(e) => handleInputChange(e)}
                    fluid
                />
                <Button loading={loading} floated="right" positive type='submit' content='Submit' />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' />
            </Form>
        </>
    );
}

export default observer(NoteForm);