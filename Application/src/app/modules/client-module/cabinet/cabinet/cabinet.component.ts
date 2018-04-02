import { Component, OnInit, Inject } from '@angular/core';
import { Rent, RentSummary } from '../../../../models/rent';
import { CabinetService } from '../../../../services/cabinet-service';

@Component({
  selector: 'app-cabinet',
  templateUrl: './cabinet.component.html',
  styleUrls: ['./cabinet.component.css']
})
export class CabinetComponent implements OnInit {
  rent:Rent;
  rentSummary:RentSummary;
  constructor(@Inject(CabinetService)private cabinetService :CabinetService) {
    this.rent=new Rent(); //hack
    this.rentSummary=new RentSummary();//hack
   }

  ngOnInit() {
    this.cabinetService.GetLastRent().subscribe(x=>{
      this.rent=x.data as Rent;
    })
    this.cabinetService.GetSummary().subscribe(x=>{
      this.rentSummary=x.data as RentSummary;
    })
  }
  
}
