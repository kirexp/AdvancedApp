import { Component, OnInit, Inject } from '@angular/core';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { Remote } from '../../../../services/http-client';
import { AuthManager } from '../../../../services/auth-manager';

@Component({
  selector: 'app-rent-history',
  templateUrl: './rent-history.component.html',
  styleUrls: ['./rent-history.component.css']
})
export class RentHistoryComponent implements OnInit {
    customersData: any;
    shippersData: any;
    dataSource: any;
    url: string;
    masterDetailDataSource: any;
/**
 *
 */

 employees: Employee[] = [
     {
    "ID": 1,
    "FirstName": "John",
    "LastName": "Heart",
    "Prefix": "Mr.",
    "Position": "CEO",
    "Picture": "images/employees/01.png",
    "BirthDate": "1964/03/16",
    "HireDate": "1995/01/15",
    "Notes": "John has been in the Audio/Video industry since 1990. He has led DevAv as its CEO since 2003.\r\n\r\nWhen not working hard as the CEO, John loves to golf and bowl. He once bowled a perfect game of 300.",
    "Address": "351 S Hill St.",
    "State": "California",
    "City": "Los Angeles",
    "Tasks": [{
        "ID": 5,
        "Subject": "Choose between PPO and HMO Health Plan",
        "StartDate": "2013/02/15",
        "DueDate": "2013/04/15",
        "Status": "In Progress",
        "Priority": "High",
        "Completion": 75
    }, {
        "ID": 6,
        "Subject": "Google AdWords Strategy",
        "StartDate": "2013/02/16",
        "DueDate": "2013/02/28",
        "Status": "Completed",
        "Priority": "High",
        "Completion": 100
    }, {
        "ID": 7,
        "Subject": "New Brochures",
        "StartDate": "2013/02/17",
        "DueDate": "2013/02/24",
        "Status": "Completed",
        "Priority": "Normal",
        "Completion": 100
    }, {
        "ID": 22,
        "Subject": "Update NDA Agreement",
        "StartDate": "2013/03/14",
        "DueDate": "2013/03/16",
        "Status": "Completed",
        "Priority": "High",
        "Completion": 100
    }, {
        "ID": 52,
        "Subject": "Review Product Recall Report by Engineering Team",
        "StartDate": "2013/05/17",
        "DueDate": "2013/05/20",
        "Status": "Completed",
        "Priority": "High",
        "Completion": 100
    }]
}]
  constructor(@Inject(Remote) private remote:Remote,private authManager:AuthManager) {
    this.url = this.remote.baseUri+'/Cabinet';

    this.dataSource = AspNetData.createStore({
        key: "id",
        loadUrl: this.url +"/GetMyRentHistory",
        onBeforeSend:(method, ajaxOptions)=>{
            ajaxOptions.headers = { 'Authorization': 'Bearer '+this.authManager.JwtToken };
        },
    });
   }

  ngOnInit() {
  }

}
export class Employee {
  ID: number;
  FirstName: string;
  LastName: string;
  Prefix: string;
  Position: string;
  Picture: string;
  BirthDate: string;
  HireDate: string;
  Notes: string;
  Address: string;
  State: string;
  City: string;
  Tasks: Task[];
}export class Task {
  ID: number;
  Subject: string;
  StartDate: string;
  DueDate: string;
  Status: string;
  Priority: string;
  Completion: number;
}