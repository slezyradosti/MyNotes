import { ChangeEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { Button, Form } from "semantic-ui-react";
import { observer } from "mobx-react-lite";

function PageForm() {
    const { pageStore, unitStore } = useStore();
    const { selectedElement, loading, createOne,
        updateOne, closeForm } = pageStore;

    const initialState = selectedElement ?? {
        name: '',
        unitId: unitStore.selectedElement?.id ?? ''
    };

    const [pageDto, setPageDto] = useState(initialState);

    function handleSubmit() {
        pageDto.id ? updateOne(pageDto) : createOne(pageDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setPageDto({ ...pageDto, [name]: value });
    }

    return (
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input palceholder='Name' value={pageDto.name}
                    name='name' onChange={handleInputChange} />
                <p aria-readonly>{pageDto.createdAt}</p>
                <Button loading={loading} floated="right" positive type='submit' content='Submit' />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' />
                <Form.TextArea disabled hidden value={pageDto.id} name='id' />
                <Form.TextArea disabled hidden value={unitStore.selectedElement?.id} name='unitId' />
            </Form>
        </>
    );
}

export default observer(PageForm);