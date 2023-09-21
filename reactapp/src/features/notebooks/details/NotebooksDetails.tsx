import { Button, Card } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";

function NotebookDetails() {
    const { notebookStore } = useStore();
    const { cancelSelectedNotebook, selectedNotebook } = notebookStore

    if (!selectedNotebook) return <LoadingComponent />;

    return (
        <Card>
            <Card.Content>
                <Card.Header>{selectedNotebook.name}</Card.Header>
                <Card.Meta>{selectedNotebook.createdAt}</Card.Meta>
            </Card.Content>
            <Card.Content extra>
                <Button onClick={() => cancelSelectedNotebook()} basic color='grey' content='Close' />
            </Card.Content>
        </Card>
    );
}

export default NotebookDetails;