import { createContext, useContext } from "react";
import NotebookStore from "./notebookStore";
import UnitStore from "./unitStore";
import PageStore from "./pageStore";
import NoteStore from "./noteStore";
import UserStore from "./userStore";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import PhotoStore from "./photoStore";
import NoteExtensionSotre from "./noteExtensionStore";
import ProfileStore from "./profileStore";

interface Store {
    notebookStore: NotebookStore
    unitStore: UnitStore
    pageStore: PageStore
    noteStore: NoteStore
    userStore: UserStore
    commonStore: CommonStore
    modalStore: ModalStore
    photoStore: PhotoStore
    noteExtensionStore: NoteExtensionSotre
    profileStore: ProfileStore
}

export const store: Store = {
    notebookStore: new NotebookStore(),
    unitStore: new UnitStore(),
    pageStore: new PageStore(),
    noteStore: new NoteStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    modalStore: new ModalStore(),
    photoStore: new PhotoStore(),
    noteExtensionStore: new NoteExtensionSotre(),
    profileStore: new ProfileStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}