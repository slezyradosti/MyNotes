import { observer } from "mobx-react-lite";
import SideNavList from "./SideNavList";
import { useStore } from "../../stores/store";
import { useEffect, useState } from "react";
import NotebookStore from "../../stores/notebookStore";
import UnitStore from "../../stores/unitStore";
import { Divider } from 'semantic-ui-react'

interface Props {
    currentEntityName: string;
    setCurrentEntityName: (name: string) => void;
    closeNav: () => void;
}

function SideNav({ currentEntityName, closeNav, setCurrentEntityName }: Props) {
    const { notebookStore, unitStore } = useStore(); // provide all entitites for the sidebarlist
    //const { openForm, selectOne, deleteOne, getArray, loading } = notebookStore;
    //TODO 
    // the function will be changed for each list (notebook, 
    // units or pages).
    //const [createForm, setCreateForm] = useState(() => () => notebookStore.openForm);

    // const [currentEntityName, setCurrentEntityName] = useState('Notebook'); //entityname. CALL WHEN DISPLAY DETAILS //transfered to App
    const [currentEntity, setCurrentEntity] = useState<NotebookStore | UnitStore>(notebookStore); //entity
    const [parentEntity, setParentEntity] = useState<NotebookStore | UnitStore>();

    useEffect(() => {
        switch (currentEntityName) {
            case 'Notebook':
                setCurrentEntity(notebookStore);
                break;
            case 'Unit':
                setCurrentEntity(unitStore);
                console.log("Changed to UNIT")
                unitStore.loadUnits();
                //?
                setParentEntity(notebookStore);
                break;
            case 'Page':
                //setCurrentEntity(notebookStore);

                //?
                setParentEntity(unitStore);
                break;
            default:
                console.log('Wrong value: ' + currentEntityName);
                break;
        }
    }, [currentEntityName])

    return (
        <>
            <div id="mySidenav" className="sidenav">
                <div>
                    <a className="createbtn" onClick={() => setCurrentEntityName('Notebook')} >Back</a>
                    {/* use to back button */}

                    <a className="closebtn" onClick={closeNav}>x</a>

                    <SideNavList
                        entityArray={currentEntity.getArray}
                        entityLoading={currentEntity.loading}
                        entityOpenForm={currentEntity.openForm}
                        selectEntity={currentEntity.selectOne}
                        deleteEntity={currentEntity.deleteOne}
                    />
                </div>
                <Divider>
                    <a onClick={() => currentEntity.openForm()} >+ Add new</a>
                </Divider>
            </div >
        </>
    );
}

export default observer(SideNav);