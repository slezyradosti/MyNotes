import { ChangeEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { Button, Form } from "semantic-ui-react";
import { observer } from "mobx-react-lite";

function UnitForm() {
    const { unitStore, notebookStore } = useStore();
    const { createOne, closeForm, loading } = unitStore;

    const initialState = {
        name: '',
        notebookId: notebookStore.selectedElement?.id ?? ''
    };

    const [unitDto, setUnitDto] = useState(initialState);

    function handleSubmit() {
        createOne(unitDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setUnitDto({ ...unitDto, [name]: value });
    }

    return (
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input
                    required={true}
                    autoFocus
                    palceholder='Name'
                    value={unitDto.name}
                    name='name'
                    onChange={handleInputChange}
                    fluid
                />
                <Button loading={loading} floated="right" type='submit' content='Submit' className="submitBtnColor" />
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' className="cancelBtnColor" />
                <br />
                <br />
            </Form>
        </>
    );
}

export default observer(UnitForm);