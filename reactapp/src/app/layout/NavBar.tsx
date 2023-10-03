import { Dropdown, Icon, Image, Menu } from 'semantic-ui-react';
import { useStore } from '../stores/store';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import userimage from '../../assets/userimage.png'

interface Props {
    openNav: () => void;
}

function NavBar({ openNav }: Props) {
    const { userStore: { user, logout } } = useStore();

    return (
        <Menu inverted fixed='top'>
            <Menu.Item header>
                <a className='sidenavOpen' id='sidenavOpen' onClick={openNav} >
                    <Icon name='bars' size='large' />
                </a>
            </Menu.Item>
            <Menu.Item position='right'>
                <Image avatar spaced='right' src={userimage} width="20" height="20" />
                <Dropdown pointing='top right' text={user?.displayName}>
                    <Dropdown.Menu>
                        <Dropdown.Item as={Link} to={`/profile/${user?.id}`} text='My Profile' icon='user' />
                        <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                    </Dropdown.Menu>
                </Dropdown>
            </Menu.Item>
        </Menu>
    );
}

export default observer(NavBar);