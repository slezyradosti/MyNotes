import { ChangeEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { Button, Form } from "semantic-ui-react";
import { observer } from "mobx-react-lite";

function UnitForm() {
    const { unitStore, notebookStore } = useStore();
    const { selectedElement, createOne, updateOne,
        closeForm, loading } = unitStore;

    const initialState = selectedElement ?? {
        name: '',
        notebookId: notebookStore.selectedElement?.id ?? ''
    };

    const [unitDto, setUnitDto] = useState(initialState);

    function handleSubmit() {
        unitDto.id ? updateOne(unitDto) : createOne(unitDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setUnitDto({ ...unitDto, [name]: value });
    }

    return (
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input palceholder='Name' value={unitDto.name}
                    name='name' onChange={handleInputChange} />
                <p aria-readonly>{unitDto.createdAt}</p>
                <Button loading={loading} floated="right" positive type='submit' content='Submit' />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' />
                <Form.TextArea disabled hidden value={unitDto.id} name='id' />
                <Form.TextArea disabled hidden value={notebookStore.selectedElement?.id} name='notebookId' />
            </Form>
        </>
    );
}

export default observer(UnitForm);