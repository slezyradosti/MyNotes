import { Grid } from "semantic-ui-react";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";

function ProfilePage() {
    const { id } = useParams<{ id: string }>();
    const { profileStore } = useStore();
    const { profile, loadingProfile, loadProfile } = profileStore;

    useEffect(() => {
        if (id) {
            loadProfile(id);
        }
    }, [loadProfile, id])

    if (loadingProfile) return <LoadingComponent content='Loading profile...' />

    return (
        <>
            <div id='main'>
                <div style={{ marginTop: '6em' }}>
                    <Grid>
                        <Grid.Column width={16}>
                            {profile &&
                                <ProfileHeader profile={profile} />}
                            <ProfileContent />
                        </Grid.Column>
                    </Grid>
                </div>
            </div>
        </>
    );
}

export default observer(ProfilePage);