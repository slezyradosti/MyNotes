import { Container, Icon, Menu } from 'semantic-ui-react';
import menu from '../../assets/menu.png'

interface Props {
    openNav: () => void;
}

function NavBar({ openNav }: Props) {
    return (
        <Menu inverted fixed='top'>
            <Menu.Item header>
                <a className='sidenavOpen' id='sidenavOpen' onClick={openNav} >
                    <Icon name='bars' size='large' />
                </a>
            </Menu.Item>
        </Menu>
    );
}

export default NavBar