import { Link } from "react-router-dom";
import { Button, Container, Header } from "semantic-ui-react";

function HomePage() {
    return (
        <Container style={{ marginTop: '7em' }}>
            <Header>
                Notes
            </Header>
            <Button as={Link} to='/login' size='big' inverted>
                Login
            </Button>
        </Container>
    );
}

export default HomePage;