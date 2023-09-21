import { Button, Container, Menu } from 'semantic-ui-react';
import { useStore } from '../stores/store';

function NavBar() {
    const { notebookStore } = useStore();

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <a href='notebooks'>
                        <img src="/assets/menu.png" alt="menu" style={{ marginRight: '10px' }} />
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