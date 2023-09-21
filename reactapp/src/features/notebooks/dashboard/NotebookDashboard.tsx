import { Grid } from "semantic-ui-react";
import { Notebook } from "../../../app/models/notebook";
import NotebookList from "./NotebookList";
import NotebookForm from "../form/NotebookForm";
import NotebookDetails from "../details/NotebooksDetails";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

interface Props {
    notebooks: Notebook[];
    ifSubmitting: boolean;
    createOrEdit: (notebook: Notebook) => void;
    deleteNotebook: (id: string) => void;
}

function NotebookDashboard({ notebooks, createOrEdit,
    deleteNotebook, ifSubmitting }: Props) {
    const { notebookStore } = useStore();
    const { selectedNotebook, editMode } = notebookStore;

    return (
        <Grid>
            <Grid.Column width='5'>
                <NotebookList
                    notebooks={notebooks}
                    deleteNotebook={deleteNotebook}
                    ifSubmitting={ifSubmitting}
                />
            </Grid.Column>
            <Grid.Column width='7'>
                {selectedNotebook && !editMode &&
                    <NotebookDetails />}
            </Grid.Column>
            <Grid.Column width='4'>
                {editMode &&
                    <NotebookForm
                        createOrEdit={createOrEdit}
                        ifSubmitting={ifSubmitting}
                    />}
            </Grid.Column>
        </Grid>
    );
}

export default observer(NotebookDashboard);