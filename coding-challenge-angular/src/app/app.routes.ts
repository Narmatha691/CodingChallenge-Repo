import { Routes } from '@angular/router';
import { RegisterComponent } from './Pages/register/register.component';
import { LoginComponent } from './Pages/login/login.component';
import { UserDashboardComponent } from './Pages/user-dashboard/user-dashboard.component';
import { EditProfileComponent } from './Pages/edit-profile/edit-profile.component';
import { AllTasksComponent } from './Pages/all-tasks/all-tasks.component';
import { AddTaskComponent } from './Pages/add-task/add-task.component';
import { EditTaskComponent } from './Pages/edit-task/edit-task.component';

export const routes: Routes = [
    { path:'',component:LoginComponent},
    { path:'register',component:RegisterComponent},
    { path:'login',component:LoginComponent},
    { path:'user-dashboard',component:UserDashboardComponent,
    children: [
      { path:'',component:AllTasksComponent},
      { path:'edit-profile',component:EditProfileComponent},
      { path:'add-task',component:AddTaskComponent},
      { path:'all-tasks',component:AllTasksComponent},
      { path:'edit-task/:tid',component:EditTaskComponent},
      ]
    },
];
