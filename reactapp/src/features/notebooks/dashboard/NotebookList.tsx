import { Button, Item } from "semantic-ui-react";
import { Notebook } from "../../../app/models/notebook";
import { SyntheticEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";

interface Props {
  notebooks: Notebook[];
  ifSubmitting: boolean;
  deleteNotebook: (id: string) => void;
}

function NotebookList({ notebooks, deleteNotebook, ifSubmitting }: Props) {
  const [target, setTarget] = useState('');
  const { notebookStore } = useStore();
  const { openForm, selectNotebook } = notebookStore;

  function handleNotebookDelete(e: SyntheticEvent<HTMLButtonElement>, id: string) {
    setTarget(e.currentTarget.name);
    deleteNotebook(id);
  }

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
                <Button
                  name={notebook.id}
                  loading={ifSubmitting && target == notebook.id}
                  onClick={(e) => handleNotebookDelete(e, notebook.id)}
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
