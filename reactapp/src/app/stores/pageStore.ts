import { makeAutoObservable, runInAction } from "mobx";
import { Page } from "../models/page";
import ISidebarListStore from "./ISidebarListStore";
import agent from "../api/agent";
import { v4 as uuid } from 'uuid';
import moment from "moment";

class PageStore implements ISidebarListStore {
    pageRegistry = new Map<string, Page>();
    selectedElement: Page | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    get getArray() {
        return Array.from(this.pageRegistry.values());
    }

    get getEntityType() {
        return 'Page';
    }

    loadPages = async (nbId: string) => {
        try {
            const pages = await agent.Pages.list(nbId);
            pages.forEach(page => {
                page.createdAt = page.createdAt?.split('T')[0];
                this.pageRegistry.set(page.id!, page);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    selectOne = (id: string) => {
        this.selectedElement = this.pageRegistry.get(id);
    }

    getOne = (id: string) => {
        return this.pageRegistry.get(id)!;
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    openForm = (id?: string) => {
        id ? this.selectOne(id) : this.cancelSelectedElement();
        this.editMode = true;
    }

    closeForm = () => {
        this.editMode = false;
    }

    createOne = async (page: Page) => {
        this.loading = true;
        page.id = uuid();

        try {
            await agent.Pages.create(page);
            page.createdAt = moment().format('YYYY-MM-DD');
            runInAction(() => {
                this.pageRegistry.set(page.id!, page);
                this.selectedElement = page;
                this.editMode = false;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    updateOne = async (page: Page) => {
        this.loading = true;

        try {
            await agent.Pages.update(page);
            runInAction(() => {
                this.pageRegistry.set(page.id!, page);
                this.selectedElement = page;
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    deleteOne = async (id: string) => {
        this.loading = true;

        try {
            await agent.Pages.delete(id);
            runInAction(() => {
                this.pageRegistry.delete(id);
                if (this.selectedElement?.id === id) this.cancelSelectedElement();
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }
}

export default PageStore;