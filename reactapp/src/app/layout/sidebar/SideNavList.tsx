import { Dropdown, Grid, Item } from "semantic-ui-react";
import { SyntheticEvent, useState } from "react";
import { observer } from "mobx-react-lite";
import { Notebook } from "../../models/notebook";
import { Unit } from "../../models/unit";

interface Props {
  entityArray: Notebook[] | Unit[];
  entityType: string;
  entityLoading: boolean;
  entityOpenForm: (id?: string | undefined) => void;
  selectEntity: (id: string) => void;
  deleteEntity: (id: string) => Promise<void>;

  setCurrentEntityName: (name: string) => void;
}

function SideNavList({ entityArray, entityLoading, entityOpenForm,
  selectEntity, deleteEntity, setCurrentEntityName, entityType }: Props) {
  const [target, setTarget] = useState('');

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
                      <a
                        onClick={() => handleSelectEntity(entity.id!)}
                        className="notebook-link"
                        style={{ wordWrap: 'break-word' }} // Enable text wrapping
                      >
                        {entity.name}
                      </a>
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
                    <Dropdown.Item onClick={() => entityOpenForm(entity.id)}
                      style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                      content='Edit'
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
