import { Component, OnInit, Inject } from '@angular/core';
import { Rent, RentSummary } from '../../../../models/rent';
import { CabinetService } from '../../../../services/cabinet-service';

@Component({
  selector: 'app-cabinet',
  templateUrl: './cabinet.component.html',
  styleUrls: ['./cabinet.component.css']
})
export class CabinetComponent implements OnInit {

  constructor(@Inject(CabinetService)private cabinetService :CabinetService) {
   }

  ngOnInit() {
  }
  
}
