import { Button, Card } from "semantic-ui-react";
import { Notebook } from "../../../app/models/notebook";

interface Props {
    notebook: Notebook;
    cancelSelectedNotebook: () => void;
}

function NotebookDetails({ notebook, cancelSelectedNotebook }: Props) {
    return (
        <Card>
            <Card.Content>
                <Card.Header>{notebook.name}</Card.Header>
                <Card.Meta>{notebook.createdAt}</Card.Meta>
            </Card.Content>
            <Card.Content extra>
                <Button onClick={() => cancelSelectedNotebook()} basic color='grey' content='Close' />
            </Card.Content>
        </Card>
    );
}

export default NotebookDetails;