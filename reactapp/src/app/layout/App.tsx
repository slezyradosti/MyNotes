import { useEffect, useState } from "react";
import { Container, } from "semantic-ui-react";
import NavBar from "./NavBar";
import NotebookDashboard from "../../features/notebooks/dashboard/NotebookDashboard";
import LoadingComponent from "./LoadingComponent";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import SideNav from "./SideNav";

function App() {
  const { notebookStore } = useStore();

  const [ifOpenSideMenu, setIfOpenSideMenu] = useState(false);

  useEffect(() => {
    notebookStore.loadNotebooks();
  }, [notebookStore]);

  if (notebookStore.laoadingInital) return <LoadingComponent content='Loading app' />

  /* Set the width of the side navigation to 250px and the left margin of the page content to 250px */
  function openNav() {
    document.getElementById("mySidenav")!.style.width = "275px";
    document.getElementById("main")!.style.marginLeft = "275px";

    setIfOpenSideMenu(true);
  }

  /* Set the width of the side navigation to 0 and the left margin of the page content to 0 */
  function closeNav() {
    document.getElementById("mySidenav")!.style.width = "0";
    document.getElementById("main")!.style.marginLeft = "0";

    setIfOpenSideMenu(false);
  }

  return (
    <>
      <NavBar openNav={openNav} />

      <div>

      </div>
      <div id="main">
        <Container style={{ marginTop: '7em' }}>
          <SideNav closeNav={closeNav} />
          <NotebookDashboard />
        </Container>
      </div>
    </>
  );
}

export default observer(App);
