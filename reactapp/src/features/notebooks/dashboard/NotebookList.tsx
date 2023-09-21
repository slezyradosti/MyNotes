import { Button, Dropdown, Item, Menu } from "semantic-ui-react";
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

  const [activeItem, setActiveItem] = useState('account');

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

                <Menu secondary vertical widths={10}>
                  <Dropdown item text='' style={{ color: 'grey' }}>
                    <Dropdown.Menu>
                      <Dropdown.Item onClick={() => openForm(notebook.id)}>
                        Edit
                      </Dropdown.Item>
                      <Dropdown.Item
                        name={notebook.id}
                        loading={loading && target == notebook.id}
                        onClick={(e) => handleNotebookDelete(e, notebook.id!)}
                        content='Delete'
                      >
                        Delete
                      </Dropdown.Item>
                    </Dropdown.Menu>
                  </Dropdown>
                </Menu>

              </Item.Description>
              <Item.Group style={{ color: 'grey', marginTop: '-5px' }}>
                {notebook.createdAt}
              </Item.Group>

              {/* <Menu secondary vertical>
                <Dropdown item text='...' style={{ color: 'grey' }}>
                  <Dropdown.Menu>
                    <Dropdown.Item onClick={() => openForm(notebook.id)}>
                      Edit
                    </Dropdown.Item>
                    <Dropdown.Item
                      name={notebook.id}
                      loading={loading && target == notebook.id}
                      onClick={(e) => handleNotebookDelete(e, notebook.id!)}
                      content='Delete'
                    >
                      Delete
                    </Dropdown.Item>
                  </Dropdown.Menu>
                </Dropdown>
              </Menu> */}
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </>
  );
}

export default observer(NotebookList);
