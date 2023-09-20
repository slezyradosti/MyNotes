import { Button, Item } from "semantic-ui-react";
import { Notebook } from "../../../app/models/notebook";

interface Props {
  notebooks: Notebook[];
  selectNotebook: (id: string) => void;
  openForm: (id: string) => void;
  deleteNotebook: (id: string) => void;
}

function NotebookList({ notebooks, selectNotebook,
  openForm, deleteNotebook }: Props) {

  return (
    <>
      <Item.Group devided>
        {notebooks.map((notebook) => (
          <Item key={notebook.id}>
            <Item.Content>
              <Item.Header>
                <Button onClick={() => selectNotebook(notebook.id)}
                  content={notebook.name} />
              </Item.Header>
              <Item.Meta>{notebook.createdAt}</Item.Meta>
              <Item.Extra>
                <Button onClick={() => openForm(notebook.id)}
                  content='Edit' />
                <Button onClick={() => deleteNotebook(notebook.id)}
                  content='Delete' />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </>
  );
}

export default NotebookList;
