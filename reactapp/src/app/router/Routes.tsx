import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'notebooks', element: <HomePage /> },
            { path: 'notebooks/:id', element: <HomePage /> },
        ]
    },
]

export const router = createBrowserRouter(routes)