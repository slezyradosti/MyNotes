import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { Grid, Item } from "semantic-ui-react";
import { useEffect } from "react";
import Note from "../../../app/models/note";

interface Props {
    noteArray: Note[];
}

function NoteList({ noteArray }: Props) {
    const { noteStore } = useStore();


    return (
        <>
            <Item.Group divided>
                <Grid>
                    {noteArray.map((note) => (
                        <Grid.Row key={note.id}>
                            <Grid.Column width={4}>
                                <Item key={note.id}>
                                    <Item.Content>
                                        <Item.Header>
                                            {note.name}
                                        </Item.Header>
                                        <Item.Description>
                                            {note.record}
                                        </Item.Description>
                                        <Item.Meta>
                                            {note.createdAt}
                                        </Item.Meta>
                                    </Item.Content>
                                </Item>
                            </Grid.Column>
                        </Grid.Row>
                    ))}
                </Grid>
            </Item.Group>
        </>
    );
}

export default observer(NoteList);