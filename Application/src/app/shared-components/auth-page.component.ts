import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators,FormArray,FormBuilder} from '@angular/forms';
import { AuthManager } from '../services/auth-manager';
import {Router} from '@angular/router';
import { debug } from 'util';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.css']
})
export class AuthPageComponent implements OnInit {
 public myForm:FormGroup;
  constructor(private authManager:AuthManager,private router:Router,) {
    this.myForm=new FormGroup({
      name:new FormControl('',[
        Validators.required, 
    ]),
      password: new FormControl('',[
        Validators.required
      ])
    })
   }

  ngOnInit() {
  }
  submit(form:FormGroup){
    debugger;
    if(form.value.name!=''&&form.value.password!=''){
      this.authManager.Authintithicate(form.value.name,form.value.password);
      this.router.navigateByUrl('/employee')
    }
  }
}
