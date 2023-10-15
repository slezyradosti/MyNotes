import { observer } from "mobx-react-lite";
import { Icon } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import ImageList from "../../photos/ImageList";

interface Props {
    noteId: string;
    deletePhotoFromRecord: (noteId: string, photoUrl: string) => Promise<void>;
}

function ImageListButton({ noteId, deletePhotoFromRecord }: Props) {
    const { modalStore, photoStore } = useStore();

    const openPhotoListHandler = () => {
        photoStore.loadPhotos(noteId);
        modalStore.openModal(<ImageList noteId={noteId} deletePhotoFromRecord={deletePhotoFromRecord} />)
    }

    return (
        <a
            className="closebtn"
            onClick={() => openPhotoListHandler()}
        >
            <Icon name="images" size='large' title='How to style' color='black' inverted />
        </a>
    );
}

export default observer(ImageListButton);