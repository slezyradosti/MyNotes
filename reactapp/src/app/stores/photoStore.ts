import { makeAutoObservable, runInAction } from "mobx";
import { Photo } from "../models/photo";
import agent from "../api/agent";
import { toast } from "react-toastify";

class PhotoStore {
    photoRegistry = new Map<string, Photo>();
    tempPhotoRegistry = new Map<string, Photo>();
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

    get getArray() {
        return Array.from(this.photoRegistry.values());
    }

    clearData = () => {
        this.photoRegistry.clear();
        this.setLoadingInitial(true);
    }

    loadPhotos = async (noteId: string) => {
        this.clearData();

        try {
            const photos = await agent.Photos.list(noteId);
            photos.forEach(photo => {
                this.photoRegistry.set(photo.id!, photo);
            });
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    private tempLoadPhotos = async (noteId: string) => {
        this.tempPhotoRegistry.clear();

        try {
            const photos = await agent.Photos.list(noteId);
            photos.forEach(photo => {
                this.tempPhotoRegistry.set(photo.id!, photo);
            });
        } catch (error) {
            console.log(error);
        }
    }

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
            this.selectOne(photo.id);
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
            runInAction(() => {
                this.photoRegistry.delete(id);
            })
        } catch (error) {
            toast.error('Problem deleting photo');
        } finally {
            runInAction(() => this.loading = false);
            this.cancelSelectedElement();
        }
    }

    checkIfPhotosWereDeleted = async (noteId: string, record: string) => {
        try {
            await this.tempLoadPhotos(noteId);
            this.findAndDeleteRemovedPhotos(Array.from(this.tempPhotoRegistry.values()), record);
        } catch (error) {
            console.log(error);
        }
    }

    private findAndDeleteRemovedPhotos = async (photoArray: Photo[], record: string) => {
        try {
            photoArray.forEach(photo => {
                if (!record.includes(photo.url)) {
                    this.selectOne(photo.id);
                    this.deletePhoto(photo.id);
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
}

export default PhotoStore;