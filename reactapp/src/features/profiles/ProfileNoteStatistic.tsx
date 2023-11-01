import { useEffect } from "react";
import { useStore } from "../../app/stores/store";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { observer } from "mobx-react-lite";
import StatisticLineChart from "./StatisticLineChart";
import { Header, Item } from "semantic-ui-react";

function ProfileNoteStatistic() {
    const { noteStore } = useStore();
    const { loadingStatistic, creationStatisticArray, loadStatistic } = noteStore

    useEffect(() => {
        loadStatistic();
    }, [])

    return (
        <>
            {loadingStatistic ?
                < LoadingComponent content='Loading statistic...' inverted={true} /> :
                (
                    <>
                        <div style={{ textAlign: "center" }}>
                            <Header content="Note Statistic" />
                            <Item.Content> How many notes were created per day </Item.Content>
                        </div>

                        <StatisticLineChart data={creationStatisticArray} />
                    </>
                )
            }
        </>
    );
}

export default observer(ProfileNoteStatistic);