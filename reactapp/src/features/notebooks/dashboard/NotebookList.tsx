import { Button, Item } from "semantic-ui-react";
import { SyntheticEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

function NotebookList() {
  const [target, setTarget] = useState('');
  const { notebookStore } = useStore();
  const { openForm, selectNotebook, deleteNotebook, notebooksArray, loading } = notebookStore;

  function handleNotebookDelete(e: SyntheticEvent<HTMLButtonElement>, id: string) {
    setTarget(e.currentTarget.name);
    deleteNotebook(id);
  }

  return (
    <>
      <Item.Group devided>
        {notebooksArray.map((notebook) => (
          <Item key={notebook.id}>
            <Item.Content>
              <Item.Header>
                <Button onClick={() => selectNotebook(notebook.id!)}
                  content={notebook.name} />
              </Item.Header>
              <Item.Meta>{notebook.createdAt}</Item.Meta>
              <Item.Extra>
                <Button onClick={() => openForm(notebook.id)}
                  content='Edit' />
                <Button
                  name={notebook.id}
                  loading={loading && target == notebook.id}
                  onClick={(e) => handleNotebookDelete(e, notebook.id!)}
                  content='Delete' />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </>
  );
}

export default observer(NotebookList);
