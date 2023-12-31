import { ChangeEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { Button, Form } from "semantic-ui-react";
import { observer } from "mobx-react-lite";

function PageForm() {
    const { pageStore, unitStore } = useStore();
    const { loading, createOne, closeForm } = pageStore;

    const initialState = {
        name: '',
        unitId: unitStore.selectedElement?.id ?? ''
    };

    const [pageDto, setPageDto] = useState(initialState);

    function handleSubmit() {
        createOne(pageDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setPageDto({ ...pageDto, [name]: value });
    }

    return (
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input
                    required={true}
                    autoFocus
                    palceholder='Name'
                    value={pageDto.name}
                    name='name'
                    onChange={handleInputChange}
                    //onBlur={() => closeForm()}
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

export default observer(PageForm);