import { User } from "./user";

export interface IProfile {
    username: string;
    displaName: string;
    bio?: string;
}

export class Profile implements IProfile {
    constructor(user: User) {
        this.username = user.username;
        this.displaName = user.displayName;
    }
    a
    username: string;
    displaName: string;
    bio?: string | undefined;
}