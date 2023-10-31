import { useEffect } from "react";
import { useStore } from "../../app/stores/store";
import { Grid, Item } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";

function ProfileNotebookStatistic() {
    const { notebookStore } = useStore();
    const { loadingStatistic, creationStatisticArray, loadStatistic } = notebookStore

    useEffect(() => {
        loadStatistic();
    }, [])


    if (loadingStatistic) return <LoadingComponent content="Loading statistic" />

    return (
        <>
            <Grid>
                {creationStatisticArray.map(statistic => (
                    <Grid.Row>
                        <Grid.Column width={2}>
                            Count: {statistic.count}
                        </Grid.Column>
                        <Grid.Column width={4}>
                            Date: {statistic.date}
                        </Grid.Column>
                    </Grid.Row>
                ))}
            </Grid>
        </>
    );
}

export default ProfileNotebookStatistic;