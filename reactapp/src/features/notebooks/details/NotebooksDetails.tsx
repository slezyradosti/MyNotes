import { Button, Card } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";

interface Props {
    setCurrentEntityName: (name: string) => void;
}

function NotebookDetails({ setCurrentEntityName }: Props) {
    const { notebookStore } = useStore();
    const { cancelSelectedElement, selectedElement } = notebookStore

    setCurrentEntityName("Unit");

    if (!selectedElement) return <LoadingComponent />;

    return (
        <Card>
            <Card.Content>
                <Card.Header>{selectedElement.name}</Card.Header>
                <Card.Meta>{selectedElement.createdAt}</Card.Meta>
            </Card.Content>
            <Card.Content extra>
                <Button onClick={() => cancelSelectedElement()} basic color='grey' content='Close' />
            </Card.Content>
        </Card>
    );
}

export default NotebookDetails;