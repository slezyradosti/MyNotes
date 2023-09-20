import { Button, Container, Menu } from 'semantic-ui-react';

interface Props {
    openForm: () => void;
}

function NavBar({ openForm }: Props) {
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <a href='notebooks'>
                        <img src="/assets/menu.png" alt="menu" style={{ marginRight: '10px' }} />
                    </a>
                </Menu.Item>
                <Menu.Item>
                    <Button onClick={openForm} positive content='Create Notebok' />
                </Menu.Item>
            </Container>
        </Menu>
    );
}

export default NavBar