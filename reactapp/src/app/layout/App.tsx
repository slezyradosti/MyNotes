import { useEffect } from "react";
import { Button, Container, Icon, Label, } from "semantic-ui-react";
import NavBar from "./NavBar";
import Dashboard from "../../features/dashboard/Dashboard";
import LoadingComponent from "./LoadingComponent";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import SideNav from "./sidebar/SideNav";
import AddNoteButton from "../../features/notes/other/AddNoteButton";
import { Outlet } from "react-router-dom";

function App() {
  const { notebookStore, pageStore } = useStore();

  useEffect(() => {
    notebookStore.loadNotebooks();
  }, [notebookStore]);

  if (notebookStore.laoadingInitial) return <LoadingComponent content='Loading app' />

  /* Set the width of the side navigation to 250px and the left margin of the page content to 250px */
  function openNav() {
    document.getElementById("mySidenav")!.style.width = "225px";
    document.getElementById("main")!.style.marginLeft = "225px";
  }

  /* Set the width of the side navigation to 0 and the left margin of the page content to 0 */
  function closeNav() {
    document.getElementById("mySidenav")!.style.width = "0";
    document.getElementById("main")!.style.marginLeft = "0";
  }

  return (
    <>
      <NavBar openNav={openNav} />
      <SideNav closeNav={closeNav} />
      <Container fluid>
        <div id="main">
          <div style={{ marginTop: '4em' }}>
            {pageStore.selectedElement &&
              <AddNoteButton />
            }
          </div>
          <div style={{ marginTop: '2em' }}>
            <Dashboard />
          </div>
        </div>
      </Container>

      <Outlet />
    </>
  );
}

export default observer(App);
