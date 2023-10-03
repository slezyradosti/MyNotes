import { Icon } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

function ColumnButton() {
    const { noteStore } = useStore();

    return (
        <a className="closebtn" onClick={() => noteStore.changeColumnsCount()} style={{ color: '#bfbfbf', marginRight: '1em' }}>
            <Icon name='columns' size='big' title={`To ${noteStore.columnsCount} table(s) view`} />
        </a>
    );
}

export default observer(ColumnButton);