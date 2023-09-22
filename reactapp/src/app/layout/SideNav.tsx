import { observer } from "mobx-react-lite";
import NotebookList from "../../features/notebooks/dashboard/NotebookNavList";
import { useStore } from "../stores/store";

interface Props {
    closeNav: () => void;
}

function SideNav({ closeNav }: Props) {
    const { notebookStore } = useStore();

    return (
        <>
            <div id="mySidenav" className="sidenav">
                <a className="createbtn" onClick={() => notebookStore.openForm()} > +</a>
                <a className="closebtn" onClick={closeNav}>x</a>

                <NotebookList />
            </div >
        </>
    );
}

export default observer(SideNav);