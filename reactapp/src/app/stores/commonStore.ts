import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";

class CommonStore {
    error: ServerError | null = null;
    token: string | null = localStorage.getItem('jwt');
    appLoaded = false;

    constructor() {
        makeAutoObservable(this);

        //doesn't happen initially (not for the first initialisation). 
        //only when the value changes
        reaction(
            () => this.token,
            token => {
                if (token) {
                    localStorage.setItem('jwt', token);
                } else {
                    localStorage.removeItem('jwt');
                }
            }
        )
    }

    setServerError(error: ServerError) {
        this.error = error;
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }
}

export default CommonStore;