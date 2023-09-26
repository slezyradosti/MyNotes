import { Dropdown, Grid, Input, Item, Divider } from "semantic-ui-react";
import { SyntheticEvent, useEffect, useRef, useState } from "react";
import { observer } from "mobx-react-lite";
import { Notebook } from "../../models/notebook";
import { Unit } from "../../models/unit";
import { Page } from "../../models/page";

interface Props {
  entityArray: Notebook[] | Unit[] | Page[];
  entityType: string;
  entityLoading: boolean;
  entityOpenForm: (id?: string | undefined) => void;
  selectEntity: (id: string) => void;
  deleteEntity: (id: string) => Promise<void>;
  updateOne: ((notebook: Notebook) => Promise<void>) | ((unit: Unit) => Promise<void>) | ((page: Page) => Promise<void>);
  getOne: (id: string) => Notebook | Unit | Page;

  setCurrentEntityName: (name: string) => void;
}

function SideNavList({ entityArray, entityLoading, entityOpenForm,
  selectEntity, deleteEntity, setCurrentEntityName,
  entityType, getOne, updateOne }: Props) {
  const [target, setTarget] = useState('');

  const [editingId, setEditingId] = useState<string | null>(null);
  const [editedName, setEditedName] = useState("");
  const inputRef = useRef<Input | null>(null);

  // changes focus to the editing name
  useEffect(() => {
    if (inputRef.current) {
      inputRef.current.focus();
    }
  }, [editingId]);

  function handleDeleteEntity(e: SyntheticEvent<HTMLButtonElement>, id: string) {
    setTarget(e.currentTarget.name);
    deleteEntity(id);
  }

  function handleSelectEntity(entityId: string) {
    switch (entityType) {
      case 'Notebook':
        setCurrentEntityName('Unit');
        break;
      case 'Unit':
        setCurrentEntityName('Page');
        break;
      case 'Page':
        //setCurrentEntityName('Notebook'); //??????
        break;
      default:
        console.log('Entity type doesn\'t exists: ' + entityType)
        break;
    }

    selectEntity(entityId);
  }

  const handleNameEditStart = (entityId: string, currentName: string) => {
    setEditingId(entityId);
    setEditedName(currentName);
  };

  const handleNameEditCancel = () => {
    setEditingId(null);
    setEditedName("");
  };

  const handleNameEditSave = (event: SyntheticEvent, entityId: string) => {
    event.preventDefault()
    // Here you can update the name in your data or send it to your API
    let newEntity = getOne(entityId);
    newEntity.name = editedName;
    updateOne(newEntity);

    //cleaning 
    handleNameEditCancel();
  };

  return (
    <>
      <Item.Group divided>
        <Grid>
          {entityArray.map((entity) => (
            <Grid.Row key={entity.id}>
              <Grid.Column width={10}>
                <Item key={entity.id}>
                  <Item.Content>
                    <Item.Description className="notebook-description">
                      {editingId === entity.id ? (
                        <Input
                          className="notebook-link"
                          ref={(input) => (inputRef.current = input)}
                          value={editedName}
                          onChange={(e) => setEditedName(e.target.value)}
                          action={{
                            icon: "check",
                            onMouseDown: (e) => handleNameEditSave(e, entity.id!), //onMouseDown will cause before onBlur (als because of event.preventDefault)
                          }}
                          onBlur={() => handleNameEditCancel()}
                          fluid
                        />
                      ) : (
                        <a
                          className="notebook-link"
                          onClick={() => handleSelectEntity(entity.id!)}
                          style={{ wordWrap: 'break-word' }}
                        >
                          {entity.name}
                        </a>
                      )}
                    </Item.Description>
                    <Item.Group className="notebook-info" style={{ color: 'grey', marginTop: '-5px', fontSize: '11x' }}>
                      {entity.createdAt}
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
                    <Dropdown.Item
                      style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                      content='Edit'
                      onClick={() => handleNameEditStart(entity.id!, entity.name)}
                    >

                    </Dropdown.Item>
                    <Dropdown.Item style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                      name={entity.id}
                      loading={entityLoading && (target === entity.id)}
                      onClick={(e) => handleDeleteEntity(e, entity.id!)}
                      content='Delete'
                    >
                    </Dropdown.Item>
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

export default observer(SideNavList);
