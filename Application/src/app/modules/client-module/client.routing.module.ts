import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../services/auth-guard';
import { RentHistoryComponent } from './cabinet/rent-history/rent-history.component';
import { CabinetComponent } from './cabinet/cabinet/cabinet.component';

const routes: Routes = [
  {path:'**',component:CabinetComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
