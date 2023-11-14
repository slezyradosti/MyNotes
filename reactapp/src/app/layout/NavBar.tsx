import { Dropdown, Icon, Image, Menu } from 'semantic-ui-react';
import { useStore } from '../stores/store';
import { Link, useNavigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import userimage from '../../assets/userimage.png'
import { useState } from 'react';

interface Props {
    openNav: () => void;
    closeNav: () => void;
}

function NavBar({ openNav, closeNav }: Props) {
    const { userStore: { user, logout } } = useStore();
    const [isSideBarOpened, setIsSideBarOpened] = useState(false);
    const navigate = useNavigate();

    function handleOpenOrCloseNav() {
        if (!isSideBarOpened) openNav()
        else closeNav();
        setIsSideBarOpened(!isSideBarOpened);
    }

    return (
        <>

            <Menu inverted fixed='top' style={{ display: 'flex', justifyContent: 'space-between' }} stackable borderless>

                <Menu.Item header >
                    <a className='sidenavOpen' id='sidenavOpen' onClick={() => handleOpenOrCloseNav()} >
                        <Icon name='bars' size='large' title='Open/Close Sidebar' />
                    </a>
                </Menu.Item>
                <Menu.Item position='right' >
                    <a onClick={() => navigate("notebooks") }>
                        <Icon name='sticky note outline' style={{ color: 'white', fontSize: '2em' }} />
                        <label className='nameTitle'>MyNotes </label>
                    </a>
                </Menu.Item>
                <Menu.Item position='right'>
                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <Image avatar spaced='right' src={userimage} width="20" height="20" />
                        <Dropdown pointing='top right' text={user?.displayName}>
                            <Dropdown.Menu>
                                <Dropdown.Item onClick={closeNav} as={Link} to={`/profiles/${user?.id}`} text='My Profile' icon='user' />
                                <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                            </Dropdown.Menu>
                        </Dropdown>
                    </div>
                </Menu.Item>

            </Menu >




        </>
    );
}

export default observer(NavBar);