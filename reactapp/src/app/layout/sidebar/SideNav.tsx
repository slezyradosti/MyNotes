import { observer } from "mobx-react-lite";
import SideNavList from "./SideNavList";
import { useStore } from "../../stores/store";
import { useEffect, useState } from "react";
import NotebookStore from "../../stores/notebookStore";
import UnitStore from "../../stores/unitStore";
import { Divider } from 'semantic-ui-react'
import PageStore from "../../stores/pageStore";

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
                        ←
                    </a>

                    <a className="closebtn" onClick={closeNav} style={{ color: '#bfbfbf' }}>x</a>

                    <SideNavList
                        entityArray={currentEntity.getArray}
                        entityLoading={currentEntity.loading}
                        entityOpenForm={currentEntity.openForm}
                        selectEntity={currentEntity.selectOne}
                        deleteEntity={currentEntity.deleteOne}
                        updateOne={currentEntity.updateOne}
                        setCurrentEntityName={setCurrentEntityName}
                        entityType={currentEntity.getEntityType}
                        getOne={currentEntity.getOne}
                        entityEditMode={currentEntity.editMode}
                    />
                </div>

                {/* <Divider>
                    <a onClick={() => currentEntity.openForm()} style={{ color: '#bfbfbf' }}>+ Add new</a>
                </Divider> */}
            </div >
        </>
    );
}

export default observer(SideNav);