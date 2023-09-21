import axios, { AxiosResponse } from "axios";
import { Notebook } from "../models/notebook";

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
    headers: { Authorization: 'Bearer ' + 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImphY2siLCJuYW1laWQiOiJkN2YwYmExOS03ZWVmLTRiYjAtMWQxNC0wOGRiYTM2MTlmMGMiLCJlbWFpbCI6ImphY2tAdGFjay5jb20iLCJuYmYiOjE2OTUwMjY2MTEsImV4cCI6MTY5NTYzMTQxMSwiaWF0IjoxNjk1MDI2NjExfQ.Zk0cT1tkyp5-LkqUI0KgYDKJnL2Om5pJRBUhJnxvjygvcFEAVKa1CdNDX_mBCi-44gm-ICYiPnWWKm7hxhVhMA' }
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

const agent = {
    Notebooks
}

export default agent;