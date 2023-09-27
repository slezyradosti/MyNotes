import { createContext, useContext } from "react";
import NotebookStore from "./notebookStore";
import UnitStore from "./unitStore";
import PageStore from "./pageStore";
import NoteStore from "./noteStore";

interface Store {
    notebookStore: NotebookStore
    unitStore: UnitStore
    pageStore: PageStore
    noteStore: NoteStore
}

export const store: Store = {
    notebookStore: new NotebookStore(),
    unitStore: new UnitStore(),
    pageStore: new PageStore(),
    noteStore: new NoteStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}