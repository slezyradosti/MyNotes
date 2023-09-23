import { Grid } from "semantic-ui-react";
import NotebookForm from "../form/NotebookForm";
import NotebookDetails from "../details/NotebooksDetails";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

interface Props {
    setCurrentEntityName: (name: string) => void;
}

function NotebookDashboard({ setCurrentEntityName }: Props) {
    const { notebookStore } = useStore();
    const { selectedElement, editMode } = notebookStore;

    return (
        <Grid>
            <Grid.Column width={12}>
                {selectedElement && !editMode &&
                    <NotebookDetails setCurrentEntityName={setCurrentEntityName} />}
            </Grid.Column>
            <Grid.Column width={4}>
                {editMode &&
                    <NotebookForm />}
            </Grid.Column>
        </Grid>
    );
}

export default observer(NotebookDashboard);