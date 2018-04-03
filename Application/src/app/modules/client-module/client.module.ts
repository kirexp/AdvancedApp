import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientRoutingModule } from './client.routing.module';
import { DxDataGridModule } from 'devextreme-angular';
import { CurrentRentComponent } from './cabinet/current-rent/current-rent.component';
import { RentHistoryComponent } from './cabinet/rent-history/rent-history.component';
import { CabinetComponent } from './cabinet/cabinet/cabinet.component';
import { CabinetService } from '../../services/cabinet-service';
import { BaloonContentComponent } from '../../shared-components/baloon-content/baloon-content.component';
import { ReserveManger } from '../../services/reserve-manager';
@NgModule({
  imports: [
    CommonModule,
    ClientRoutingModule,
    DxDataGridModule,
  ],
  bootstrap:[CurrentRentComponent],
  declarations: [CurrentRentComponent,RentHistoryComponent,CabinetComponent,BaloonContentComponent],
  providers:[CabinetService,ReserveManger]
})
export class ClientModule { }
