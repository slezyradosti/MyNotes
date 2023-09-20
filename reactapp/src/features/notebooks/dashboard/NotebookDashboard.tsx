import { Grid } from "semantic-ui-react";
import { Notebook } from "../../../app/models/notebook";
import NotebookList from "./NotebookList";
import NotebookForm from "../form/NotebookForm";
import NotebookDetails from "../details/NotebooksDetails";

interface Props {
    notebooks: Notebook[];
    selectedNotebok: Notebook | undefined;
    selectNotebook: (id: string) => void;
    cancelSelectedNotebook: () => void;
    editMode: boolean;
    ifSubmitting: boolean;
    openForm: (id: string) => void;
    closeForm: () => void;
    createOrEdit: (notebook: Notebook) => void;
    deleteNotebook: (id: string) => void;
}

function NotebookDashboard({ notebooks, selectedNotebok,
    selectNotebook, cancelSelectedNotebook,
    editMode, openForm, closeForm, createOrEdit,
    deleteNotebook, ifSubmitting }: Props) {
    return (
        <Grid>
            <Grid.Column width='5'>
                <NotebookList
                    notebooks={notebooks}
                    selectNotebook={selectNotebook}
                    openForm={openForm}
                    deleteNotebook={deleteNotebook}
                />
            </Grid.Column>
            <Grid.Column width='7'>
                {selectedNotebok && !editMode &&
                    <NotebookDetails notebook={selectedNotebok}
                        cancelSelectedNotebook={cancelSelectedNotebook}
                    />}
            </Grid.Column>
            <Grid.Column width='4'>
                {editMode &&
                    <NotebookForm
                        notebook={selectedNotebok}
                        closeForm={closeForm}
                        createOrEdit={createOrEdit}
                        ifSubmitting={ifSubmitting}
                    />}
            </Grid.Column>
        </Grid>
    );
}

export default NotebookDashboard;