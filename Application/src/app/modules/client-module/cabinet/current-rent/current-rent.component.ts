import { Component, OnInit, Inject } from '@angular/core';
import { Rent, RentSummary } from '../../../../models/rent';
import { CabinetService } from '../../../../services/cabinet-service';
@Component({
  selector: 'app-current-rent',
  templateUrl: './current-rent.component.html',
  styleUrls: ['./current-rent.component.css']
})
export class CurrentRentComponent implements OnInit {
  public lastRent:Rent;
  rentSummary:RentSummary;
  constructor(@Inject(CabinetService)private cabinetService :CabinetService) {
    this.lastRent=new Rent(); //hack
    this.rentSummary=new RentSummary();//hack
   }

  ngOnInit() {
    this.cabinetService.GetLastRent().subscribe(x=>{
      this.lastRent=x.data as Rent;
    })
    this.cabinetService.GetSummary().subscribe(x=>{
      this.rentSummary=x.data as RentSummary;
    })
  }

}
