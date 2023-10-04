import { Container, } from "semantic-ui-react";
import NavBar from "./NavBar";
import Dashboard from "../../features/dashboard/Dashboard";
import { observer } from "mobx-react-lite";
import SideNav from "./sidebar/SideNav";
import { Outlet, useLocation } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ModalContainer from "../common/modals/ModalContainer";

function App() {
  const location = useLocation();

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
      <ModalContainer />
      {
        location.pathname === '/' ? <HomePage />
          : (
            <>
              <NavBar openNav={openNav} />
              <SideNav closeNav={closeNav} />
              <Container fluid>
                <Outlet />
              </Container>
            </>
          )
      }
    </>
  );
}

export default observer(App);
