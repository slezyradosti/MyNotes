import { Link } from "react-router-dom";
import { Button, Container, Header } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoginForm from "../users/LoginForm";

function HomePage() {
    const { userStore, modalStore } = useStore();

    return (
        <Container style={{ marginTop: '7em' }}>
            <Header>
                Notes
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
                    <Button onClick={() => modalStore.openModal(<h1 content="register" />)} size='big' inverted>
                        Register
                    </Button>
                </>

            )}
        </Container>
    );
}

export default observer(HomePage);