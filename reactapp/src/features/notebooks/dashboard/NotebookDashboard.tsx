import { Grid } from "semantic-ui-react";
import NotebookForm from "../form/NotebookForm";
import NotebookDetails from "../details/NotebooksDetails";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import UnitForm from "../../units/form/UnitForm";
import PageForm from "../../pages/form/PageForm";

function NotebookDashboard() {
    const { notebookStore, unitStore, pageStore } = useStore();
    const { selectedElement, editMode } = notebookStore;

    return (
        <Grid>
            <Grid.Column width={12}>
                {/* {selectedElement && !editMode &&
                    <NotebookDetails />} */}
            </Grid.Column>
            <Grid.Column width={4}>
                {editMode &&
                    <NotebookForm />}
                {unitStore.editMode &&
                    <UnitForm />}
                {pageStore.editMode &&
                    <PageForm />}
            </Grid.Column>
        </Grid>
    );
}

export default observer(NotebookDashboard);