import { Notebook } from "../models/notebook";
import { Page } from "../models/page";
import { Unit } from "../models/unit";

interface ISidebarListStore {
    loading: boolean;
    openForm: (id?: string | undefined) => void;
    selectOne: (id: string) => void;
    deleteOne: (id: string) => Promise<void>;
    getOne: (id: string) => Notebook | Unit | Page;
}

export default ISidebarListStore;