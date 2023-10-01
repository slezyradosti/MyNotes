import { makeAutoObservable, runInAction } from "mobx";
import { Unit } from "../models/unit";
import agent from "../api/agent";
import { v4 as uuid } from 'uuid';
import ISidebarListStore from "./ISidebarListStore";
import moment from "moment";

class UnitStore implements ISidebarListStore {
    unitRegistry = new Map<string, Unit>();
    selectedElement: Unit | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    get getArray() {
        return Array.from(this.unitRegistry.values());
    }

    get getEntityType() {
        return 'Unit';
    }

    loadUnits = async (nbId: string) => {
        try {
            const units = await agent.Units.list(nbId);
            units.forEach(unit => {
                unit.createdAt = unit.createdAt?.split('T')[0];
                this.unitRegistry.set(unit.id!, unit);
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
