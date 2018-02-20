import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './services/auth-guard';
import { AuthPageComponent } from './shared-components/auth-page.component';
import { AppComponent } from './app.component';


const routes: Routes = [
  {path:'auth-page',component:AuthPageComponent},
  {path:'employee',loadChildren: './modules/employee-module/employee.module#EmployeeModule',canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
