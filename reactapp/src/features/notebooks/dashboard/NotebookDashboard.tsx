import { Grid } from "semantic-ui-react";
import NotebookForm from "../form/NotebookForm";
import NotebookDetails from "../details/NotebooksDetails";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import UnitForm from "../../units/form/UnitForm";
import PageForm from "../../pages/form/PageForm";
import NoteList from "../../notes/list/NoteList";
import { useEffect } from "react";

function NotebookDashboard() {
    const { notebookStore, unitStore, pageStore, noteStore } = useStore();
    const { selectedElement, editMode } = pageStore;

    return (
        <Grid>
            <Grid.Column width={12}>
                {selectedElement && !editMode &&
                    <>
                        <NoteList
                            noteArray={noteStore.getArray}
                            noteLoading={noteStore.loading}
                            noteEditMode={noteStore.editMode}
                            noteOpenForm={noteStore.openForm}
                            noteSelect={noteStore.selectOne}
                            noteUpdate={noteStore.updateOne}
                            noteDelete={noteStore.deleteOne}
                            getNote={noteStore.getOne}
                        />
                        {console.log('display notes: ' + noteStore.getArray)}
                    </>
                }
            </Grid.Column>
            <Grid.Column width={4}>
                {/* {editMode &&
                    <NotebookForm />}
                {unitStore.editMode &&
                    <UnitForm />}
                {pageStore.editMode &&
                    <PageForm />} */}
            </Grid.Column>
        </Grid>
    );
}

export default observer(NotebookDashboard);