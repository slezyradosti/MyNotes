import { Grid } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";
import NoteList from "../notes/list/NoteList";

function Dashboard() {
    const { pageStore, noteStore } = useStore();
    const { selectedElement, editMode } = pageStore;

    return (
        <Grid>
            <Grid.Column width={16}>
                {selectedElement && !editMode &&
                    <>
                        <NoteList
                            noteArray={noteStore.getArray}
                            noteLoading={noteStore.loading}
                            noteEditMode={noteStore.editMode}
                            noteSelectedElement={noteStore.selectedElement}
                            noteOpenForm={noteStore.openForm}
                            noteSelect={noteStore.selectOne}
                            noteUpdate={noteStore.updateOne}
                            noteDelete={noteStore.deleteOne}
                            getNote={noteStore.getOne}
                            columnsCount={noteStore.columnsCount}
                        />
                        {console.log('display notes: ' + noteStore.getArray)}
                    </>
                }
            </Grid.Column>
        </Grid>
    );
}

export default observer(Dashboard);