import { Routes } from '@angular/router';
import { UserListComponent } from './components/Testing/user-list/user-list.component';
import { MovieListComponent } from './components/movie-list/movie-list.component';


export const routes: Routes = [
    {
    path: '',
    component: UserListComponent
    },
    {
        path: "movie",
        component: MovieListComponent
    }
];
