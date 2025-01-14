import { Routes } from '@angular/router';
import { UserListComponent } from './components/Testing/user-list/user-list.component';
import { MovieListComponent } from './components/movie-list/movie-list.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AdminComponent } from './components/admin/admin.component';
import { Role } from './models/user.models';
import { BookingComponent } from './components/booking/booking.component';


export const routes: Routes = [
    {
    path: '',
    component: UserListComponent
    },
    {
        path: "movies",
        component: MovieListComponent
    },
    {
        path: "login",
        component: LoginComponent
    },
    { path: 'register', component: RegisterComponent },
    {path: 'profile', component: ProfileComponent},
    {
        path: "admin",
        component: AdminComponent,
        data: { roles: [Role.Administrator] }
    },
    {
        path: 'booking',
        component: BookingComponent,
      },
      {
        path: 'booking/:movieSessionId',
        component: BookingComponent,
      },
];
