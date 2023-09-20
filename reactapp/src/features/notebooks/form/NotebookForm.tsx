import { Button, Form } from "semantic-ui-react";
import { Notebook } from "../../../app/models/notebook";
import { ChangeEvent, useState } from "react";

interface Props {
    notebook: Notebook | undefined;
    ifSubmitting: boolean;
    closeForm: () => void;
    createOrEdit: (notebook: Notebook) => void;
}

function NotebookForm({ notebook: selectedNotebook, closeForm,
    createOrEdit, ifSubmitting }: Props) {
    const initialState = selectedNotebook ?? {
        id: '',
        name: '',
    };

    const [notebookDto, setNotebookDto] = useState(initialState);

    function handleSubmit() {
        createOrEdit(notebookDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setNotebookDto({ ...notebookDto, [name]: value })
    }

    return (
        <>
            <p aria-readonly>{notebookDto.createdAt}</p>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input palceholder='Name' value={notebookDto.name} name='name' onChange={handleInputChange} />

                <Button loading={ifSubmitting} floated='right' positive type='submit' content='Submit' />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' />
                <Form.TextArea hidden value={notebookDto.id} />
                <Form.TextArea hidden value={notebookDto.userId} />
            </Form>
        </>
    );
}

export default NotebookForm;