import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators,FormArray,FormBuilder} from '@angular/forms';
import {Router} from '@angular/router';
import { debug } from 'util';
import { AuthManager } from '../../services/auth-manager';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.css']
})
export class AuthPageComponent implements OnInit {
 public myForm:FormGroup;
 public authError:string="";
  constructor(private authManager:AuthManager,private router:Router) {
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
    form.valid
    if(form.value.name!=''&&form.value.password!=''){
      let result =this.authManager.Authintithicate(form.value.name,form.value.password);
      result.subscribe(result=>{
        if(!result.IsSuccess){
          this.authError=result.AuthintithicationError;
        }else{
          this.router.navigateByUrl('/client')
        }
      },error=>{
        this.authError=error.AuthintithicationError;
      });

    }
  }
}
