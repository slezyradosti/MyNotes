import { makeAutoObservable, runInAction } from "mobx";
import { Photo } from "../models/photo";
import agent from "../api/agent";
import { toast } from "react-toastify";

class PhotoStore {
    photoRegistry = new Map<string, Photo>();
    selectedElement: Photo | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;
    uploading: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    get getEntityType() {
        return 'Photo';
    }

    // loadPhotos = async (noteId: string) => {
    //     try {
    //         //something
    //     } catch (error) {
    //         console.log(error);
    //     } finally {
    //         this.setLoadingInitial(false);
    //     }
    // }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    selectOne = (id: string) => {
        this.selectedElement = this.photoRegistry.get(id);
    }

    getOne = (id: string) => {
        return this.photoRegistry.get(id)!;
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    uploadPhoto = async (file: Blob, noteId: string) => {
        this.uploading = true;

        try {
            const response = await agent.Photos.upload(file, noteId);
            const photo = response.data;
            this.photoRegistry.set(photo.id, photo);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.uploading = false);
        }
    }

    deletePhoto = async (id: string) => {
        this.loading = true;

        try {
            await agent.Photos.delete(id);
        } catch (error) {
            toast.error('Problem deleting photo');
        } finally {
            runInAction(() => this.loading = false);
        }

    }
}

export default PhotoStore;