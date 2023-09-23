import { createContext, useContext } from "react";
import NotebookStore from "./notebookStore";
import UnitStore from "./unitStore";

interface Store {
    notebookStore: NotebookStore
    unitStore: UnitStore
}

export const store: Store = {
    notebookStore: new NotebookStore(),
    unitStore: new UnitStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}