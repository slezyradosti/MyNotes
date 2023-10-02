import { observer } from "mobx-react-lite";
import SideNavList from "./SideNavList";
import { useStore } from "../../stores/store";
import { useEffect, useState } from "react";
import NotebookStore from "../../stores/notebookStore";
import UnitStore from "../../stores/unitStore";
import { Divider, Icon } from 'semantic-ui-react'
import PageStore from "../../stores/pageStore";
import LoadingComponent from "../LoadingComponent";

interface Props {
    closeNav: () => void;
}

function SideNav({ closeNav }: Props) {
    const { notebookStore, unitStore, pageStore, noteStore } = useStore(); // provide all entitites for the sidebarlist
    const [currentEntityName, setCurrentEntityName] = useState('Notebook');
    const [currentEntity, setCurrentEntity] = useState<NotebookStore | UnitStore | PageStore>(notebookStore); //entity
    const [parentEntityName, setParentEntityName] = useState<string>('');
    const [parentEntity, setParentEntity] = useState<NotebookStore | UnitStore | PageStore | undefined>(undefined);

    useEffect(() => {
        switch (currentEntityName) {
            case 'Notebook':
                setCurrentEntity(notebookStore);
                notebookStore.loadNotebooks();

                setParentEntityName('');

                setParentEntity(undefined);
                break;
            case 'Unit':
                setCurrentEntity(unitStore);
                unitStore.loadUnits(notebookStore.selectedElement!.id!);
                //?
                setParentEntityName('Notebook');
                setParentEntity(notebookStore);
                break;
            case 'Page':
                setCurrentEntity(pageStore);
                pageStore.loadPages(unitStore.selectedElement!.id!);
                //?
                setParentEntityName('Unit');
                setParentEntity(unitStore);
                break;
            case 'Note':
                noteStore.loadNotes(pageStore.selectedElement!.id!);
                break;
            default:
                console.log('SideNav Wrong value: ' + currentEntityName);
                break;
        }
    }, [currentEntityName, notebookStore, unitStore, pageStore])

    return (
        <>
            <div id="mySidenav" className="sidenav">
                <div>
                    <a className="returnbtn" onClick={() => setCurrentEntityName(parentEntityName)} style={{ color: '#bfbfbf' }} >
                        <Icon name='arrow left' size='small' title='Back' />
                    </a>

                    <a className="closebtn" onClick={closeNav} style={{ color: '#bfbfbf' }}>
                        <Icon name='close' size='small' title='Close' />
                    </a>

                    {currentEntity?.loadingInitial ?
                        < LoadingComponent content='Loading data...' inverted={false} />
                        : (
                            <SideNavList
                                entityArray={currentEntity.getArray}
                                entityLoading={currentEntity.loading}
                                entityType={currentEntity.getEntityType}
                                entityEditMode={currentEntity.editMode}
                                entityOpenForm={currentEntity.openForm}
                                selectEntity={currentEntity.selectOne}
                                deleteEntity={currentEntity.deleteOne}
                                updateOne={currentEntity.updateOne}
                                setCurrentEntityName={setCurrentEntityName}
                                getOne={currentEntity.getOne}
                            />
                        )}
                </div>
            </div >
        </>
    );
}

export default observer(SideNav);