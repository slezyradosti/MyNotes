interface ISidebarListStore {
    loading: boolean;
    openForm: (id?: string | undefined) => void;
    selectOne: (id: string) => void;
    deleteOne: (id: string) => Promise<void>;
}

export default ISidebarListStore;