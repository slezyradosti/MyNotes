import { observer } from "mobx-react-lite";
import { Icon } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import HelpInfo from "./HelpInfo";

function HelpButton() {
    const { modalStore } = useStore();

    return (
        <a className="closebtn" onClick={() => modalStore.openModal(<HelpInfo />)} >
            <Icon name="help" size='large' title='How to style' color='black' inverted />
        </a>
    );
}

export default observer(HelpButton);