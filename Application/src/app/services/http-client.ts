import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthManager } from './auth-manager';
import { Inject } from '@angular/core';
import { debug } from 'util';

export class Remote{
    // baseUri:string="http://localhost:49432";
     baseUri:string="http://95.59.125.132:1133";
    /**
     *
     */
    constructor(@Inject(HttpClient)private httpClient: HttpClient) {}
    get(uri:string):Observable<any>{
        return this.httpClient.get(uri);
    }
    postFormData(uri:string,formData:FormData):Observable<any>{
        return this.httpClient.post(uri,formData);
    }
    post(uri:string,data:object):Observable<any>{
         let httpOptions = {
            headers: new HttpHeaders({
              'Content-Type':  'application/json',
              })}
       return this.httpClient.post(uri,data,httpOptions);
    }
}