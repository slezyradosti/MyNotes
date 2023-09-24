import { Button, Form } from "semantic-ui-react";
import { ChangeEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

function NotebookForm() {
    const { notebookStore } = useStore();
    const { selectedElement, closeForm,
        createOne, updateOne, loading } = notebookStore;

    const initialState = selectedElement ?? {
        name: '',
    };

    const [notebookDto, setNotebookDto] = useState(initialState);

    function handleSubmit() {
        notebookDto.id ? updateOne(notebookDto) : createOne(notebookDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setNotebookDto({ ...notebookDto, [name]: value });
    }

    return (
        <>

            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input palceholder='Name' value={notebookDto.name} name='name' onChange={handleInputChange} />
                <p aria-readonly>{notebookDto.createdAt}</p>
                <Button loading={loading} floated='right' positive type='submit' content='Submit' />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' />
                <Form.TextArea disabled hidden value={notebookDto.id} />
                <Form.TextArea disabled hidden value={notebookDto.userId} />
            </Form>
        </>
    );
}

export default observer(NotebookForm);