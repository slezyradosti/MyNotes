import { Link } from "react-router-dom";
import { Button, Container, Header } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";

function HomePage() {
    const { userStore } = useStore();

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
                <Button as={Link} to='/login' size='big' inverted>
                    Login
                </Button>
            )}
        </Container>
    );
}

export default observer(HomePage);