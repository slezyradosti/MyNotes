import { Grid } from "semantic-ui-react";
import ProfileHeader from "./ProfileHeader";

function ProfilePage() {
    return (
        <>
            <div id='main'>
                <div style={{ marginTop: '6em' }}>
                    <Grid>
                        <Grid.Column width={16}>
                            <ProfileHeader />
                        </Grid.Column>
                    </Grid>
                </div>
            </div>
        </>
    );
}

export default ProfilePage;