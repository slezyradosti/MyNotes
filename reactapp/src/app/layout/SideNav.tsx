import { observer } from "mobx-react-lite";
import NotebookList from "../../features/notebooks/dashboard/NotebookList";

interface Props {
    closeNav: () => void;
}

function SideNav({ closeNav }: Props) {

    return (
        <>
            <div id="mySidenav" className="sidenav">
                <a className="closebtn" onClick={closeNav}>x</a>

                <NotebookList />
            </div>
        </>
    );
}

export default observer(SideNav);