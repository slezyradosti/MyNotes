import { Dropdown, Grid, Item } from "semantic-ui-react";
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
      <Item.Group divided>
        <Grid>
          {notebooksArray.map((notebook) => (
            <Grid.Row key={notebook.id}>
              <Grid.Column width={10}>
                <Item key={notebook.id}>
                  <Item.Content>
                    <Item.Description className="notebook-description">
                      <a
                        onClick={() => selectNotebook(notebook.id!)}
                        className="notebook-link"
                        style={{ wordWrap: 'break-word' }} // Enable text wrapping
                      >
                        {notebook.name}
                      </a>
                    </Item.Description>
                    <Item.Group className="notebook-info" style={{ color: 'grey', marginTop: '-5px', fontSize: '11x' }}>
                      {notebook.createdAt}
                    </Item.Group>
                  </Item.Content>
                </Item>
              </Grid.Column>
              <Grid.Column width={6}>
                {/* Content for the right column, including dropdown */}
                <Dropdown
                  placeholder=" "
                  fluid
                  selection
                  style={{ color: '#a0a0a0', backgroundColor: 'transparent', border: 'none' }}
                >

                  <Dropdown.Menu style={{ backgroundColor: '#111111', right: 0, top: 15, border: 'none' }}>
                    <Dropdown.Header active onClick={() => openForm(notebook.id)} style={{ color: '#a0a0a0', cursor: 'pointer' }} >
                      Edit
                    </Dropdown.Header>
                    <Dropdown.Header style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                      name={notebook.id}
                      loading={loading && (target === notebook.id)}
                      onClick={(e) => handleNotebookDelete(e, notebook.id!)}
                      content='Delete'
                    >
                      Delete
                    </Dropdown.Header>
                  </Dropdown.Menu>
                </Dropdown>
              </Grid.Column>
            </Grid.Row>
          ))}
        </Grid>
      </Item.Group>
    </>
  );
}

export default observer(NotebookList);
