import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { Button, Form, TextArea } from "semantic-ui-react";
import { ChangeEvent, useState } from "react";
import HelpButton from "../other/HelpButton";

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
                <div className="field">
                    <TextArea
                        required={true}
                        autoFocus
                        placeholder='Record'
                        value={noteDto.record}
                        name='record'
                        onChange={(e) => handleInputChange(e)}

                    />
                </div>
                <div style={{ display: 'flex', justifyContent: 'end', alignItems: 'center' }}>
                    <HelpButton />
                    <Button onClick={closeForm} type='button' content='Cancel' className="cancelBtnColor Border" />
                    <Button loading={loading} type='submit' content='Submit' className="submitBtnColor Border" />
                </div>
            </Form >
        </>
    );
}

export default observer(NoteForm);