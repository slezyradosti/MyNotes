import { createContext, useContext } from "react";
import NotebookStore from "./notebookStore";
import UnitStore from "./unitStore";
import PageStore from "./pageStore";

interface Store {
    notebookStore: NotebookStore
    unitStore: UnitStore
    pageStore: PageStore
}

export const store: Store = {
    notebookStore: new NotebookStore(),
    unitStore: new UnitStore(),
    pageStore: new PageStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}