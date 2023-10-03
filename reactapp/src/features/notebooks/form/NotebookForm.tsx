import { Button, Form } from "semantic-ui-react";
import { ChangeEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

function NotebookForm() {
    const { notebookStore } = useStore();
    const { closeForm, createOne, loading } = notebookStore;

    const initialState = {
        name: '',
    };

    const [notebookDto, setNotebookDto] = useState(initialState);

    function handleSubmit() {
        createOne(notebookDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setNotebookDto({ ...notebookDto, [name]: value });
    }

    return (
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input
                    required={true}
                    autoFocus
                    palceholder='Name'
                    value={notebookDto.name}
                    name='name'
                    onChange={handleInputChange}
                    //onBlur={() => closeForm()}
                    fluid
                />
                <Button loading={loading} floated='right' type='submit' content='Submit' className="submitBtnColor" />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' className="cancelBtnColor" />
                <br />
                <br />
            </Form>
        </>
    );
}

export default observer(NotebookForm);