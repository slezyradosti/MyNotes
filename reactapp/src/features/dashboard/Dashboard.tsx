import { Grid, Loader, SemanticWIDTHS } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";
import NoteList from "../notes/list/NoteList";
import AddNoteButton from "../notes/other/AddNoteButton";
import ColumnButton from "../notes/other/ColumnButton";
import { useEffect, useState } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { PagingParams } from "../../app/models/pagination";
import InfiniteScroll from "react-infinite-scroller";

function Dashboard() {
    const { pageStore, noteStore, commonStore, userStore } = useStore();
    const { selectedElement, editMode } = pageStore;

    useEffect(() => {
        if (commonStore.token) {
            userStore.getUser().finally(() => commonStore.setAppLoaded())
        } else {
            commonStore.setAppLoaded()
        }
    }, [commonStore, userStore])

    //pagination
    const [loadingNext, setLoadingNext] = useState(false);
    function handleGetNext() {
        setLoadingNext(true);
        noteStore.setPagingParams(new PagingParams(noteStore.pagination!.PageIndex + 1))
        noteStore.loadNotes(pageStore.selectedElement!.id!).then(() => setLoadingNext(false));
    }

    if (!commonStore.appLoaded) return <LoadingComponent content="Loading app...." />

    return (
        <div id="main">
            <div style={{ marginTop: '4em' }}>
                {pageStore.selectedElement &&
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <div>
                            <AddNoteButton />
                        </div>
                        <div>
                            <ColumnButton />
                        </div>
                    </div>
                }
            </div>
            <div style={{ marginTop: '2em' }}>
                <Grid>
                    <Grid.Column width={16}>
                        {selectedElement && !editMode &&
                            <>
                                <InfiniteScroll
                                    pageStart={0}
                                    loadMore={handleGetNext}
                                    hasMore={!loadingNext && !!noteStore.pagination &&
                                        noteStore.pagination.PageIndex < noteStore.pagination.PageCount}
                                    useWindow={false}
                                >
                                    <NoteList
                                        noteArray={noteStore.getArray}
                                        noteLoading={noteStore.loading}
                                        noteEditMode={noteStore.editMode}
                                        noteSelectedElement={noteStore.selectedElement}
                                        noteLoadingInitial={noteStore.loadingInitial}
                                        noteOpenForm={noteStore.openForm}
                                        noteSelect={noteStore.selectOne}
                                        noteUpdate={noteStore.updateOne}
                                        noteDelete={noteStore.deleteOne}
                                        getNote={noteStore.getOne}
                                        columnsCount={noteStore.columnsCount as SemanticWIDTHS}
                                        loadingNext={loadingNext}
                                        cancelSelectedNote={noteStore.cancelSelectedElement}
                                    />
                                </InfiniteScroll>

                            </>
                        }
                    </Grid.Column>
                </Grid>
            </div>

        </div>
    );
}

export default observer(Dashboard);