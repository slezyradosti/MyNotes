import { Button, Grid, Header, Icon, Item } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";

interface Props {
    noteId: string;
    deletePhotoFromRecord: (noteId: string, photoUrl: string) => Promise<void>;
}

function ImageList({ noteId, deletePhotoFromRecord }: Props) {
    const { photoStore } = useStore();
    const { getArray, loading, getOne, selectedElement } = photoStore;

    useEffect(() => {
        photoStore.loadPhotos(noteId);
    }, [photoStore]);

    const handleDeletePhoto = (photId: string) => {
        const photo = getOne(photId)
        deletePhotoFromRecord(noteId, photo.url);
        //deletePhoto(photId);
    }

    return (
        <>
            <Item.Group>
                <Item.Content>
                    <Item.Header>
                        <Header> The list contains photo you uploaded</Header>
                    </Item.Header>
                    <Item.Description>
                        You can delete photos by clicking a trash icon or just remove a full link inside the note.
                    </Item.Description>
                </Item.Content>
            </Item.Group>
            <Grid columns={2}>
                {getArray.map((photo) => (
                    <Grid.Row key={photo.id}>
                        <Grid.Column>
                            <img src={photo.url} className="modalListImage"></img>
                        </Grid.Column>
                        <Grid.Column>
                            <div>
                                <Button
                                    key={photo.id}
                                    style={{ backgroundColor: 'transparent' }}
                                    floated="right"
                                    loading={loading && selectedElement?.id === photo.id}
                                    onClick={() => handleDeletePhoto(photo.id)}
                                >
                                    <Icon name='trash' title='Delete Image' />
                                </Button>
                            </div>
                        </Grid.Column>
                    </Grid.Row>
                ))}
            </Grid>
        </>
    );
}

export default observer(ImageList);