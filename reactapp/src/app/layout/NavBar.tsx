import { Container, Menu } from 'semantic-ui-react';
import menu from '../../assets/menu.png'

interface Props {
    openNav: () => void;
}

function NavBar({ openNav }: Props) {
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <a className='sidenavOpen' id='sidenavOpen' onClick={openNav} >
                        <img src={menu} alt="menu" style={{ width: '25px' }} />
                    </a>
                </Menu.Item>
            </Container>
        </Menu>
    );
}

export default NavBar