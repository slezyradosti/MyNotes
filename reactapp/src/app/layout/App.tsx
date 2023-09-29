import { useEffect } from "react";
import { Container, } from "semantic-ui-react";
import NavBar from "./NavBar";
import NotebookDashboard from "../../features/notebooks/dashboard/NotebookDashboard";
import LoadingComponent from "./LoadingComponent";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import SideNav from "./sidebar/SideNav";

function App() {
  const { notebookStore } = useStore();
  //const [currentEntityName, setCurrentEntityName] = useState('Notebook'); //entityname. CALL WHEN DISPLAY DETAILS

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
            Create note button
          </div>
          <div style={{ marginTop: '7em' }}>
            <NotebookDashboard />
          </div>
        </div>
      </Container>
    </>
  );
}

export default observer(App);
