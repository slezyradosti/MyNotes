import { makeAutoObservable, runInAction } from "mobx";
import { Profile } from "../models/profile";
import agent from "../api/agent";

class ProfileStore {
    profile: Profile | null = null;
    loadingProfile = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadProfile = async (id: string) => {
        this.loadingProfile = true;
        try {
            const profile = await agent.Profiles.details(id);
            console.log('profile ' + profile);
            runInAction(() => {
                this.profile = profile;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.loadingProfile = false);
        }
    }
}

export default ProfileStore;