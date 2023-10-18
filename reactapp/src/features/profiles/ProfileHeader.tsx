import { Divider, Grid, Header, Item, Reveal, Segment, Statistic } from "semantic-ui-react";
import userimage from '../../assets/userimage.png'

function ProfileHeader() {
    return (
        <Segment>
            <Grid>
                <Grid.Column width={12}>
                    <Item.Group>
                        <Item>
                            <Item.Image avatar size='small' src={userimage} />
                            <Item.Content verticalAlign='middle'>
                                <Header as='h1' content='DisplayName' />
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Grid.Column>
                <Grid.Column width={4}>
                    <Statistic.Group widths={2}>
                        <Statistic label='Created notebooks' value='7' />
                        <Statistic label='Created notes' value='77' />
                    </Statistic.Group>
                    <Divider />
                    <Reveal animated='move'>
                        <Reveal.Content visible style={{ width: '100%' }}>
                            this was for following
                        </Reveal.Content>
                    </Reveal>
                </Grid.Column>
            </Grid>
        </Segment>
    );
}

export default ProfileHeader;