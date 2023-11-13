import { observer } from "mobx-react-lite";
import SidenavList from "./SideNavList";
import { useStore } from "../../stores/store";
import { useEffect, useState } from "react";
import NotebookStore from "../../stores/notebookStore";
import UnitStore from "../../stores/unitStore";
import { Divider, Icon } from 'semantic-ui-react'
import PageStore from "../../stores/pageStore";
import LoadingComponent from "../LoadingComponent";
import { useNavigate } from "react-router-dom";
import { PagingParams } from "../../models/pagination";
import InfiniteScroll from "react-infinite-scroller";
import { Notebook } from "../../models/notebook";
import { Unit } from "../../models/unit";
import { Page } from "../../models/page";
import Note from "../../models/note";

interface Props {
    closeNav: () => void;
}

function Sidenav({ closeNav }: Props) {
    const { notebookStore, unitStore, pageStore, noteStore } = useStore(); // provide all entitites for the sidebarlist
    const [currentEntityName, setCurrentEntityName] = useState('Notebook');
    const [currentEntity, setCurrentEntity] = useState<NotebookStore | UnitStore | PageStore>(notebookStore); //entity
    const [parentEntityName, setParentEntityName] = useState<string>('');
    const navigate = useNavigate();

    //pagination
    const [loadingNext, setLoadingNext] = useState(false);
    function handleGetNext() {
        setLoadingNext(true);
        currentEntity.setPagingParams(new PagingParams(currentEntity.pagination!.PageIndex + 1))
        handleLoadNextData();
    }

    function handleLoadNextData() {
        switch (currentEntityName) {
            case 'Notebook':
                return notebookStore.loadData().then(() => setLoadingNext(false));
            case 'Unit':
                return unitStore.loadData(notebookStore.selectedElement!.id!).then(() => setLoadingNext(false));
            case 'Page':
                return pageStore.loadData(unitStore.selectedElement!.id!).then(() => setLoadingNext(false));
            case 'Note':
                return noteStore.loadNotes(pageStore.selectedElement!.id!).then(() => setLoadingNext(false));
            default:
                break;
        }
    }

    useEffect(() => {
        switch (currentEntityName) {
            case 'Notebook':
                setCurrentEntity(notebookStore);
                notebookStore.loadData();

                // redirect to notebooks/
                navigate('notebooks');

                setParentEntityName('');
                //setParentEntity(undefined);

                //when user returns back
                unitStore.cancelSelectedElement();
                break;
            case 'Unit':
                setCurrentEntity(unitStore);
                navigate('units'); // redirect to units
                //setSearchParams({ ndId: notebookStore.selectedElement!.id! }); // add query parameter: units?ndId=123
                unitStore.loadData(notebookStore.selectedElement!.id!);
                setParentEntityName('Notebook');
                // setParentEntity(notebookStore);

                //when user returns back
                pageStore.cancelSelectedElement();
                break;
            case 'Page':
                setCurrentEntity(pageStore);
                navigate('pages'); //redirect to pages
                //setSearchParams({ unitId: unitStore.selectedElement!.id! }) // add query parameter: pages?unitId=123

                pageStore.loadData(unitStore.selectedElement!.id!);
                setParentEntityName('Unit');
                // setParentEntity(unitStore);

                //when user returns back
                noteStore.cancelSelectedElement();
                break;
            case 'Note':
                navigate('notes'); // redirect to notes?pageId=123
                //setSearchParams({ pageId: pageStore.selectedElement!.id! }) // add query parameter: notes?pageId=123
                //
                noteStore.setPagingParams(new PagingParams(0)) // IMPORTANT. as there is no redirecting with different query string for now, we Reset pageIndex value as new Note is loading
                noteStore.loadNotes(pageStore.selectedElement!.id!);
                setCurrentEntityName('clearValue'); // IMPORTANT. clearing current entityname value to making able load new  
                break;
            case 'clearValue':
                break;
            default:
                console.log('SideNav Wrong value: ' + currentEntityName);
                break;
        }
    }, [currentEntityName])


    const handleUpdateOne = (entity: Notebook | Unit | Page): Promise<void> => {
        if (currentEntityName == 'Notebook') return notebookStore.updateOne(entity);
        else if (currentEntityName == 'Unit') return unitStore.updateOne(entity as Unit);
        else return pageStore.updateOne(entity as Page);
    };

    return (
        <>
            <div id="mySidenav" className="sidenav">
                <div style={{ justifyContent: 'space-between', display: 'flex' }}>
                    <a className="returnbtn" onClick={() => setCurrentEntityName(parentEntityName)} style={{ color: '#bfbfbf' }} >
                        <Icon name='arrow left' size='small' title='Back' />
                    </a>

                    <a className="closebtn" onClick={closeNav} style={{ color: '#bfbfbf' }}>
                        <Icon name='close' size='small' title='Close' />
                    </a>
                </div>

                {currentEntity?.loadingInitial && !loadingNext ?
                    < LoadingComponent content='Loading data...' inverted={false} />
                    : (
                        <>

                            <div>
                                <InfiniteScroll
                                    pageStart={0}
                                    loadMore={handleGetNext}
                                    hasMore={!loadingNext && !!currentEntity.pagination &&
                                        currentEntity.pagination.PageIndex < currentEntity.pagination.PageCount - 1}
                                    useWindow={false}
                                >
                                    <SidenavList
                                        entityArray={currentEntity.getArray}
                                        entityLoading={currentEntity.loading}
                                        entityType={currentEntity.getEntityType}
                                        entityEditMode={currentEntity.editMode}
                                        entityOpenForm={currentEntity.openForm}
                                        selectEntity={currentEntity.selectOne}
                                        deleteEntity={currentEntity.deleteOne}
                                        updateOne={handleUpdateOne}
                                        setCurrentEntityName={setCurrentEntityName}
                                        getOne={currentEntity.getOne}
                                        loadingNext={loadingNext}
                                    />
                                </InfiniteScroll>

                                <div style={{ color: '#bfbfbf', position: 'sticky', bottom: '0' }}>
                                    <Divider style={{ color: '#bfbfbf' }} fitted />
                                    <a onClick={() => currentEntity.openForm()} style={{ color: '#bfbfbf', backgroundColor: "#2B2B2B", textTransform: 'uppercase' }} >
                                        <Icon name='add' size='large' />
                                        Add {currentEntity.getEntityType}
                                    </a>
                                </div>
                            </div>

                        </>
                    )}
            </div >
        </>
    );
}

export default observer(Sidenav);