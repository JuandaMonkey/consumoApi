import { Routes, provideRouter } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list.component';
import { UserListComponent } from './components/user-list/user-list.component';

export const routes: Routes = [
    {path: 'tareas', component: TaskListComponent},
    {path: 'usuarios', component: UserListComponent},
    {path: '**', redirectTo: '/tareas', pathMatch: 'full'}
];

export const appRouterProviders = [provideRouter(routes)];