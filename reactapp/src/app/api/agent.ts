import axios, { AxiosResponse } from "axios";
import { Notebook } from "../models/notebook";
import { Unit } from "../models/unit";
import { Page } from "../models/page";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    });
}

axios.defaults.baseURL = 'https://localhost:7177';

axios.interceptors.response.use(response => {
    return sleep(1000).then(() => {
        return response
    }).catch((error) => {
        console.log(error);
        return Promise.reject(error);
    });
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

//--------
//TODO
const config = {
    headers: { Authorization: 'Bearer ' + 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImphY2siLCJuYW1laWQiOiJkN2YwYmExOS03ZWVmLTRiYjAtMWQxNC0wOGRiYTM2MTlmMGMiLCJlbWFpbCI6ImphY2tAdGFjay5jb20iLCJuYmYiOjE2OTU1NzYzNjcsImV4cCI6MTY5NjE4MTE2NywiaWF0IjoxNjk1NTc2MzY3fQ.Dtn4UHshtvh_qjRNmEhnR6u1vMpHunFDHqeEDYGI74pP65ohHWBfdsladEtinSNbDgcFWI2USRkFp6wC_gJ-bA' },
    //params: { nbId: 'd8b80d57-c6fd-48d2-08ea-08dba361bf58' }
};
//-------

const requests = {
    get: <T>(url: string) => axios.get<T>(url, config).then(responseBody),
    post: <T>(url: string, body: object) => axios.post<T>(url, body, config).then(responseBody),
    put: <T>(url: string, body: object) => axios.put<T>(url, body, config).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url, config).then(responseBody),
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

const agent = {
    Notebooks,
    Units,
    Pages,
}

export default agent;