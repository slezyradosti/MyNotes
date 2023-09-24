import { createContext, useContext } from "react";
import NotebookStore from "./notebookStore";

interface Store {
    notebookStore: NotebookStore
}

export const store: Store = {
    notebookStore: new NotebookStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}