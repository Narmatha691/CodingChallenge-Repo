import { Routes } from '@angular/router';
import { RegisterComponent } from './Pages/register/register.component';
import { LoginComponent } from './Pages/login/login.component';
import { UserDashboardComponent } from './Pages/user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './Pages/admin-dashboard/admin-dashboard.component';
import { EditProfileComponent } from './Pages/edit-profile/edit-profile.component';

export const routes: Routes = [
    { path:'',component:LoginComponent},
    { path:'register',component:RegisterComponent},
    { path:'login',component:LoginComponent},
    { path:'user-dashboard',component:UserDashboardComponent,
    children: [
      { path:'edit-profile',component:EditProfileComponent},
      ]
    },
    { path:'admin-dashboard',component:AdminDashboardComponent,
    children: [
      { path:'edit-profile',component:EditProfileComponent},
      ]
    },
];
