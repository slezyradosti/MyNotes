import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { Button, Form } from "semantic-ui-react";
import { ChangeEvent, useState } from "react";

function NoteForm() {
    const { pageStore, noteStore } = useStore();
    const { loading, createOne, closeForm } = noteStore

    const initialState = {
        name: '',
        record: '',
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
                    required={true}
                    autoFocus
                    placeholder='Name'
                    value={noteDto.name}
                    name='name'
                    onChange={handleInputChange}
                    fluid
                />
                <Form.TextArea
                    required={true}
                    autoFocus
                    placeholder='Record'
                    value={noteDto.record}
                    name='record'
                    onChange={(e) => handleInputChange(e)}
                    fluid
                />
                <Button loading={loading} floated="right" type='submit' content='Submit' className="submitBtnColor Border" />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' className="cancelBtnColor Border" />
            </Form>
        </>
    );
}

export default observer(NoteForm);