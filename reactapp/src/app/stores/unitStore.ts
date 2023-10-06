import { makeAutoObservable, runInAction } from "mobx";
import { Unit } from "../models/unit";
import agent from "../api/agent";
import { v4 as uuid } from 'uuid';
import ISidebarListStore from "./ISidebarListStore";
import moment from "moment";
import { Pagination, PagingParams } from "../models/pagination";

class UnitStore implements ISidebarListStore {
    unitRegistry = new Map<string, Unit>();
    selectedElement: Unit | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();

    constructor() {
        makeAutoObservable(this);
    }

    setPagingParams = (pagingParams: PagingParams) => {
        this.pagingParams = pagingParams;
    }

    get axiosParams() {
        const params = new URLSearchParams();
        params.append('PageIndex', this.pagingParams.paramPageIndex.toString());
        params.append('PageSize', this.pagingParams.paramPageSize.toString());
        return params;
    }

    get getArray() {
        return Array.from(this.unitRegistry.values());
    }

    get getEntityType() {
        return 'Unit';
    }

    loadData = async (nbId: string) => {
        try {
            const result = await agent.Units.list(nbId, this.axiosParams);
            result.data.forEach(unit => {
                unit.createdAt = unit.createdAt?.split('T')[0];
                this.unitRegistry.set(unit.id!, unit);
            })
            this.setPagination(result.pagination);
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    selectOne = (id: string) => {
        this.selectedElement = this.unitRegistry.get(id);
    }

    getOne = (id: string) => {
        return this.unitRegistry.get(id)!;
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

    createOne = async (unit: Unit) => {
        this.loading = true;
        unit.id = uuid();

        try {
            await agent.Units.create(unit);
            unit.createdAt = moment().format('YYYY-MM-DD');
            runInAction(() => {
                this.unitRegistry.set(unit.id!, unit);
                this.selectedElement = unit;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    updateOne = async (unit: Unit) => {
        this.loading = true;

        try {
            await agent.Units.update(unit);
            runInAction(() => {
                this.unitRegistry.set(unit.id!, unit);
                this.selectedElement = unit;
                this.editMode = false;
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    deleteOne = async (id: string) => {
        this.loading = true;

        try {
            await agent.Units.delete(id);
            runInAction(() => {
                this.unitRegistry.delete(id);
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

export default UnitStore;
