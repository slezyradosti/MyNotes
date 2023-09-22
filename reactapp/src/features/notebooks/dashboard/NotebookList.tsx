import { Button, Dropdown, Item, Menu } from "semantic-ui-react";
import { SyntheticEvent, useLayoutEffect, useRef, useState } from "react";
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
      <Item.Group devided >
        {notebooksArray.map((notebook) => (
          <Item key={notebook.id}>
            <Item.Content>
              <Item.Description style={{ color: 'grey', display: 'flex' }}>
                <a onClick={() => selectNotebook(notebook.id!)}>
                  {notebook.name}
                </a>

                <Menu secondary vertical inverted widths={10} >
                  <Dropdown item text='' style={{ color: 'grey' }} >
                    <Dropdown.Menu style={{ backgroundColor: '#111111' }}>
                      <Dropdown.Header active onClick={() => openForm(notebook.id)} style={{ color: '#a0a0a0', cursor: 'pointer' }} >
                        Edit
                      </Dropdown.Header>
                      <Dropdown.Header style={{ color: '#a0a0a0', cursor: 'pointer' }}
                        name={notebook.id}
                        loading={loading && (target == notebook.id)}
                        onClick={(e) => handleNotebookDelete(e, notebook.id!)}
                        content='Delete'
                      >
                        Delete
                      </Dropdown.Header>
                    </Dropdown.Menu>
                  </Dropdown>
                </Menu>

              </Item.Description>
              <Item.Group style={{ color: 'grey', marginTop: '-5px' }}>
                {notebook.createdAt}
              </Item.Group>
            </Item.Content>
          </Item>
        ))}
      </Item.Group >
    </>
  );
}

export default observer(NotebookList);
