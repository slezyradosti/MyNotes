import { Link } from "react-router-dom";
import { Button, Container, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoginForm from "../users/LoginForm";
import RegisterForm from "../users/RegisterForm";

function HomePage() {
    const { userStore, modalStore } = useStore();

    return (
        <Segment inverted textAlign='center' vertical className='masthead' >
            <Container text style={{ marginTop: '7em' }}>
                <Header style={{ color: 'white' }}>
                    MyNotes
                </Header>
                {userStore.isLoggedIn ? (
                    <>
                        <Header as='h2' inverted content='Welcome to My Notes' />
                        <Button as={Link} to='/notebooks' size='big' inverted>
                            Go to Notebooks
                        </Button>
                    </>
                ) : (
                    <>
                        <Button onClick={() => modalStore.openModal(<LoginForm />)} size='big' inverted>
                            Login
                        </Button>
                        <Button onClick={() => modalStore.openModal(<RegisterForm />)} size='big' inverted>
                            Register
                        </Button>
                    </>

                )}
            </Container>
        </Segment>
    );
}

export default observer(HomePage);