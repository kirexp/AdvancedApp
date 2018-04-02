import { Remote } from "./http-client";
import { Observable } from "rxjs/Observable";
import { Inject } from "@angular/core";

export class ReserveManger{
    /**
     *
     */
    constructor(@Inject(Remote)private remote:Remote) {
    }
    GetFreeCars():Observable<any>{
        return this.remote.get(this.remote.baseUri+'/Reserve/GetUnReservedVehicles');
    }
}