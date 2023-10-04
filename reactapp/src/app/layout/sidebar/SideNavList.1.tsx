import { Dropdown, Grid, Input, Item, Divider, Icon } from "semantic-ui-react";
import { SyntheticEvent, useEffect, useRef, useState } from "react";
import NotebookForm from "../../../features/notebooks/form/NotebookForm";
import UnitForm from "../../../features/units/form/UnitForm";
import PageForm from "../../../features/pages/form/PageForm";
import { Link } from "react-router-dom";
import { Props } from "./SideNavList";

export function SideNavList({ entityArray, entityLoading, entityOpenForm, selectEntity, deleteEntity, setCurrentEntityName, entityType, getOne, updateOne, entityEditMode, currectEntityName }: Props) {
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
        setTarget(id);
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
                setCurrentEntityName('Note'); //??????
                break;
            default:
                console.log('Entity type doesn\'t exists: ' + entityType);
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
        event.preventDefault();
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
                        <>
                            <Grid.Row key={entity.id} columns={2}>
                                <Grid.Column width={10}>
                                    <Item key={entity.id}>
                                        <Item.Content>
                                            <Item.Description className="notebook-description">
                                                {editingId === entity.id ? (
                                                    <Input
                                                        required={true}
                                                        className="notebook-link"
                                                        ref={(input) => (inputRef.current = input)}
                                                        value={editedName}
                                                        onChange={(e) => setEditedName(e.target.value)}
                                                        action={{
                                                            icon: "check",
                                                            onMouseDown: (e) => handleNameEditSave(e, entity.id!), //onMouseDown will cause before onBlur (als because of event.preventDefault)
                                                        }}
                                                        onBlur={() => handleNameEditCancel()}
                                                        fluid />
                                                ) : (
                                                    <Link
                                                        className="notebook-link"
                                                        onClick={() => handleSelectEntity(entity.id!)}
                                                        style={{ wordWrap: 'break-word' }}
                                                        to={`${currectEntityName}s`}
                                                    >
                                                        <>
                                                            {console.log('curr name' + currectEntityName)}
                                                            {entity.name}
                                                        </>
                                                    </Link>
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
                                        className="ui selection"
                                        style={{ color: '#a0a0a0', backgroundColor: 'transparent', border: 'none' }}
                                    >
                                        <Dropdown.Menu style={{ backgroundColor: 'transparent', right: 0, top: 15, border: 'none' }}>
                                            <Dropdown.Item
                                                key={`edit ${entity.id}`}
                                                style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                                                content='Edit'
                                                onClick={() => handleNameEditStart(entity.id!, entity.name)}
                                            >
                                            </Dropdown.Item>
                                            <Dropdown.Item style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                                                key={`delete ${entity.id}`}
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
                        </>
                    ))}
                </Grid>
            </Item.Group>

            {entityEditMode && entityType === 'Notebook' &&
                <NotebookForm />}
            {entityEditMode && entityType === 'Unit' &&
                <UnitForm />}
            {entityEditMode && entityType === 'Page' &&
                <PageForm />}
            <Divider style={{ color: 'red' }}>
                <a onClick={() => entityOpenForm()} style={{ color: '#bfbfbf' }}>
                    <Icon name='add' size='large' />
                    Add {entityType}
                </a>
            </Divider>
        </>
    );
}
