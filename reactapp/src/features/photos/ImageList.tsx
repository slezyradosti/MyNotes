import { Button, Grid, Icon } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";

interface Props {
    noteId: string;
    deletePhotoFromRecord: (noteId: string, photoUrl: string) => Promise<void>;
}

function ImageList({ noteId, deletePhotoFromRecord }: Props) {
    const { photoStore } = useStore();
    const { getArray, loading, deletePhoto, getOne } = photoStore;

    useEffect(() => {
        photoStore.loadPhotos(noteId);
    }, [photoStore]);

    const handleDeletePhoto = (photId: string) => {
        deletePhoto(photId);
        const photo = getOne(photId)
        deletePhotoFromRecord(noteId, photo.url);
    }

    return (
        <Grid columns={2}>
            {getArray.map((photo) => (
                <Grid.Row key={photo.id}>
                    <Grid.Column>
                        <img src={photo.url} className="modalListImage"></img>
                    </Grid.Column>
                    <Grid.Column>
                        <div>
                            <Button
                                style={{ backgroundColor: 'transparent' }}
                                floated="right"
                                loading={loading}
                                onClick={() => handleDeletePhoto(photo.id)}
                            >
                                <Icon name='trash' title='Delete Image' />
                            </Button>
                        </div>
                    </Grid.Column>
                </Grid.Row>
            ))}
        </Grid>
    );
}

export default observer(ImageList);