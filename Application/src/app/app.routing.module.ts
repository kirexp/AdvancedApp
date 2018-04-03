import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './services/auth-guard';
import { AppComponent } from './app.component';
import { UnAuthGuard } from './services/unauthorized-guard';
import { AuthPageComponent } from './shared-components/auth.page.component/auth-page.component';
import { RegistrationPageComponent } from './shared-components/registration-page/registration-page.component';
import { HomePageComponent } from './shared-components/home-page/home-page.component';
import { CarMapComponent } from './shared-components/car-map/car-map.component';


const routes: Routes = [
  {path:'auth-page',component:AuthPageComponent,canActivate:[UnAuthGuard]},
  {path:'employee',loadChildren: './modules/employee-module/employee.module#EmployeeModule',canActivate:[AuthGuard]},
  {path:'client',loadChildren: './modules/client-module/client.module#ClientModule',canActivate:[AuthGuard]},
  {path:'register',component:RegistrationPageComponent,canActivate:[UnAuthGuard]},
  {path:'map',component:CarMapComponent,canActivate:[UnAuthGuard]},
  {path:'**',component:HomePageComponent,canActivate:[UnAuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
