import { createContext, useContext } from "react";
import NotebookStore from "./notebookStore";
import UnitStore from "./unitStore";
import PageStore from "./pageStore";
import NoteStore from "./noteStore";
import UserStore from "./userStore";
import CommonStore from "./commonStore";

interface Store {
    notebookStore: NotebookStore
    unitStore: UnitStore
    pageStore: PageStore
    noteStore: NoteStore
    userStore: UserStore
    commonStore: CommonStore
}

export const store: Store = {
    notebookStore: new NotebookStore(),
    unitStore: new UnitStore(),
    pageStore: new PageStore(),
    noteStore: new NoteStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}