import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import Dashboard from "../../features/dashboard/Dashboard";
import ProfilePage from "../../features/profiles/ProfilePage";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'login', element: <HomePage /> },
            { path: 'notebooks', element: <Dashboard /> },
            { path: 'notebooks/:id', element: <Dashboard /> },
            { path: 'units', element: <Dashboard /> },
            { path: 'units/:id', element: <Dashboard /> },
            { path: 'pages', element: <Dashboard /> },
            { path: 'pages/:id', element: <Dashboard /> },
            { path: 'notes', element: <Dashboard /> },
            { path: 'notes/:id', element: <Dashboard /> },
            { path: 'profiles/:id', element: <ProfilePage /> },
            { path: 'not-found', element: <NotFound /> },
            { path: 'server-error', element: <ServerError /> },
            { path: '*', element: <Navigate replace to='/not-found' /> },
        ]
    },
    // {
    //     path: '/login',
    //     element: <LoginForm />,
    // }
]

export const router = createBrowserRouter(routes)