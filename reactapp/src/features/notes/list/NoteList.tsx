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
        <Item.Group divided>
            <Grid columns={3} doubling stackable> {/* Enable doubling and stacking */}
                {noteArray.map((note) => (
                    <Grid.Column key={note.id}>
                        <Item key={note.id}>
                            <Item.Content>
                                <Item.Header>{note.name}</Item.Header>
                                <Item.Description>{note.record}</Item.Description>
                                <Item.Meta>{note.createdAt}</Item.Meta>
                            </Item.Content>
                        </Item>
                    </Grid.Column>
                ))}
            </Grid>
        </Item.Group>
    );
}

export default observer(NoteList);