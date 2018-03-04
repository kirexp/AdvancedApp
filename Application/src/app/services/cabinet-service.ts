import { Rent } from "../models/rent";
import { Remote } from "./http-client";
import { Observable } from "rxjs/Observable";
import { Injectable } from "@angular/core";
@Injectable()
export class CabinetService{
    /**
     *
     */
    constructor(private remote: Remote) {
        
    }
    GetLastRent():Observable<any>{
        return this.remote.get(this.remote.baseUri+"/Cabinet/GetLastRent")
    }
    GetSummary():Observable<any>{
        return this.remote.get(this.remote.baseUri+"/Cabinet/GetSummary")
    }
}