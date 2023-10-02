import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import LoginForm from "../../features/users/LoginForm";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'notebooks', element: <HomePage /> },
            { path: 'notebooks/:id', element: <HomePage /> },
            { path: 'login', element: <LoginForm /> },
        ]
    },
]

export const router = createBrowserRouter(routes)