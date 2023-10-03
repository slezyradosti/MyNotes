import axios, { AxiosError, AxiosResponse } from "axios";
import { Notebook } from "../models/notebook";
import { Unit } from "../models/unit";
import { Page } from "../models/page";
import Note from "../models/note";
import { User, UserFormValues } from "../models/user";
import { store } from "../stores/store";
import { router } from "../router/Routes";
import { toast } from "react-toastify";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    });
}

axios.defaults.baseURL = 'https://localhost:7177';

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
    return config;
})

axios.interceptors.response.use(async response => {
    await sleep(1000);
    return response;
}, (error: AxiosError) => {
    const { data, status, config } = error.response as AxiosResponse;
    switch (status) {
        case 400:
            if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
                router.navigate('/not-found');
            }
            if (data.errors) {
                const modalStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key])
                    }
                }
                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised')
            break;
        case 403:
            toast.error('forbidden')
            break;
        case 404:
            router.navigate('/not-found');
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error');
            break;
    }
    return Promise.reject(error);
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Notebooks = {
    list: () => requests.get<Notebook[]>('/notebooks'),
    details: (id: string) => requests.get<Notebook>(`/notebooks/${id}`),
    create: (notebook: Notebook) => requests.post<void>('/notebooks', notebook),
    update: (notebook: Notebook) => requests.put<void>(`/notebooks/${notebook.id}`, notebook),
    delete: (id: string) => requests.delete<void>(`/notebooks/${id}`)
}

const Units = {
    list: (nbId: string) => requests.get<Unit[]>(`/units?nbId=${nbId}`),
    details: (id: string) => requests.get<Unit>(`/units/${id}`),
    create: (unit: Unit) => requests.post<void>('/units', unit),
    update: (unit: Unit) => requests.put<void>(`/units/${unit.id}`, unit),
    delete: (id: string) => requests.delete<void>(`/units/${id}`)
}

const Pages = {
    list: (unitId: string) => requests.get<Page[]>(`/pages?unitId=${unitId}`),
    details: (id: string) => requests.get<Page>(`/pages/${id}`),
    create: (page: Page) => requests.post<void>('/pages', page),
    update: (page: Page) => requests.put<void>(`/pages/${page.id}`, page),
    delete: (id: string) => requests.delete<void>(`/pages/${id}`)
}

const Notes = {
    list: (pageId: string) => requests.get<Note[]>(`/notes?pageId=${pageId}`),
    details: (id: string) => requests.get<Note>(`/notes/${id}`),
    create: (note: Note) => requests.post<void>('/notes', note),
    update: (note: Note) => requests.put<void>(`/notes/${note.id}`, note),
    delete: (id: string) => requests.delete<void>(`/notes/${id}`)
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user: UserFormValues) => requests.post<User>('/account/login', user),
    register: (user: UserFormValues) => requests.post<User>('/account/register', user)
}

const agent = {
    Notebooks,
    Units,
    Pages,
    Notes,
    Account
}

export default agent;