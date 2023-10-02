import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import LoginForm from "../../features/users/LoginForm";

export const routes: RouteObject[] = [
    {
        //TODO
        //normal routing
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'notebooks', element: <App /> },
            { path: 'notebooks/:id', element: <App /> },
        ]
    },
    {
        path: '/login',
        element: <LoginForm />,
    }
]

export const router = createBrowserRouter(routes)