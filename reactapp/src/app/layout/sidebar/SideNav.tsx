import { observer } from "mobx-react-lite";
import SideNavList from "./SideNavList";
import { useStore } from "../../stores/store";
import { useEffect, useState } from "react";
import NotebookStore from "../../stores/notebookStore";
import UnitStore from "../../stores/unitStore";
import { Button, Divider, Icon, Loader } from 'semantic-ui-react'
import PageStore from "../../stores/pageStore";
import LoadingComponent from "../LoadingComponent";
import { useNavigate, useSearchParams } from "react-router-dom";
import { PagingParams } from "../../models/pagination";
import InfiniteScroll from "react-infinite-scroller";

interface Props {
    closeNav: () => void;
}

function SideNav({ closeNav }: Props) {
    const { notebookStore, unitStore, pageStore, noteStore } = useStore(); // provide all entitites for the sidebarlist
    const [currentEntityName, setCurrentEntityName] = useState('Notebook');
    const [currentEntity, setCurrentEntity] = useState<NotebookStore | UnitStore | PageStore>(notebookStore); //entity
    const [parentEntityName, setParentEntityName] = useState<string>('');
    const [parentEntity, setParentEntity] = useState<NotebookStore | UnitStore | PageStore | undefined>(undefined);

    const [searchParams, setSearchParams] = useSearchParams();
    //const { } = useParams();
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

    // params mostly used for IRL user input
    // doesn't fit here, makes 2x requests
    // maybe there is a better way?
    useEffect(() => {
        switch (currentEntityName) {
            case 'Notebook':
                setCurrentEntity(notebookStore);
                notebookStore.loadData();
                // redirect to notebooks/
                navigate('notebooks');

                setParentEntityName('');
                setParentEntity(undefined);
                break;
            case 'Unit':
                setCurrentEntity(unitStore);

                //
                navigate('units'); // redirect to units
                //setSearchParams({ ndId: notebookStore.selectedElement!.id! }); // add query parameter: units?ndId=123
                //?
                unitStore.loadData(notebookStore.selectedElement!.id!);
                //
                setParentEntityName('Notebook');
                setParentEntity(notebookStore);
                break;
            case 'Page':
                setCurrentEntity(pageStore);

                //
                navigate('pages'); //redirect to pages
                //setSearchParams({ unitId: unitStore.selectedElement!.id! }) // add query parameter: pages?unitId=123
                //
                pageStore.loadData(unitStore.selectedElement!.id!);
                //?
                setParentEntityName('Unit');
                setParentEntity(unitStore);
                break;
            case 'Note':

                // 
                navigate('notes'); // redirect to notes?pageId=123
                //setSearchParams({ pageId: pageStore.selectedElement!.id! }) // add query parameter: notes?pageId=123
                //
                noteStore.loadNotes(pageStore.selectedElement!.id!);
                break;
            default:
                console.log('SideNav Wrong value: ' + currentEntityName);
                break;
        }
    }, [currentEntityName, searchParams])

    return (
        <>
            <div id="mySidenav" className="sidenav">
                <a className="returnbtn" onClick={() => setCurrentEntityName(parentEntityName)} style={{ color: '#bfbfbf' }} >
                    <Icon name='arrow left' size='small' title='Back' />
                </a>

                <a className="closebtn" onClick={closeNav} style={{ color: '#bfbfbf' }}>
                    <Icon name='close' size='small' title='Close' />
                </a>

                {/* <Button onClick={handleGetNext} loading={loadingNext}>
                        next
                    </Button> */}

                {currentEntity?.loadingInitial && !loadingNext ?
                    < LoadingComponent content='Loading data...' inverted={false} />
                    : (
                        <div>
                            <InfiniteScroll
                                pageStart={0}
                                loadMore={handleGetNext}
                                hasMore={!loadingNext && !!currentEntity.pagination &&
                                    currentEntity.pagination.PageIndex < currentEntity.pagination.PageCount - 1}
                                useWindow={false}
                            >
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
                                    loadingNext={loadingNext}
                                />
                            </InfiniteScroll>
                        </div>
                    )}
            </div >
        </>
    );
}

export default observer(SideNav);