import { Grid } from "semantic-ui-react";
import NotebookList from "./NotebookList";
import NotebookForm from "../form/NotebookForm";
import NotebookDetails from "../details/NotebooksDetails";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

function NotebookDashboard() {
    const { notebookStore } = useStore();
    const { selectedNotebook, editMode } = notebookStore;

    return (
        <Grid>
            <Grid.Column width='5'>
                <NotebookList />
            </Grid.Column>
            <Grid.Column width='7'>
                {selectedNotebook && !editMode &&
                    <NotebookDetails />}
            </Grid.Column>
            <Grid.Column width='4'>
                {editMode &&
                    <NotebookForm />}
            </Grid.Column>
        </Grid>
    );
}

export default observer(NotebookDashboard);