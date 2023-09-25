import { Button, Container, Menu } from 'semantic-ui-react';
import { useStore } from '../stores/store';
import menu from '../../assets/menu.png'

interface Props {
    openNav: () => void;
}

function NavBar({ openNav }: Props) {
    const { notebookStore } = useStore();

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <a className='sidenavOpen' id='sidenavOpen' onClick={openNav} >
                        <img src={menu} alt="menu" style={{ marginRight: '10px', width: '25px' }} />
                    </a>
                </Menu.Item>
                <Menu.Item>
                    <Button onClick={() => notebookStore.openForm()} positive content='Create Notebok' />
                </Menu.Item>
            </Container>
        </Menu>
    );
}

export default NavBar