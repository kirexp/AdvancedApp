import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../services/auth-guard';
import { RentHistoryComponent } from './cabinet/rent-history/rent-history.component';
import { CabinetComponent } from './cabinet/cabinet/cabinet.component';
import { UnAuthGuard } from '../../services/unauthorized-guard';
import { CurrentRentComponent } from './cabinet/current-rent/current-rent.component';

const routes: Routes = [
   {path:'',redirectTo:'cabinet'},
  {path:'cabinet',children:[
    {
      path:'',
      component:CabinetComponent
    },
    {
      path:'timeline',
      component: RentHistoryComponent
    },
    {
      path:'condition',
      component:CurrentRentComponent
    }
  ]},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
