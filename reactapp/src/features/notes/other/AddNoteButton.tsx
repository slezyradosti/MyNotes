import { Button, Icon, Label } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

function AddNoteButton() {
    const { noteStore } = useStore();

    return (
        <Button animated='vertical' style={{ backgroundColor: 'transparent' }}>
            <Button.Content hidden >
                <Label onClick={() => noteStore.openForm()}
                    circular size='large'
                    className="addBtnColor"
                    style={{ boxShadow: '0 0 0 1px rgba(34, 36, 38, .15) inset ' }}>
                    ADD NOTE
                </Label>
            </Button.Content>
            <Button.Content visible>
                <Icon name='add' size='large' bordered backgroundcolor='grey' circular className='addBtnColor' />
            </Button.Content>
        </Button>
    );
}

export default AddNoteButton;