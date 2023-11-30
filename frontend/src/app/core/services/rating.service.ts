import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { IEmployeeQuantity } from "src/app/models/IEmployeeQuantity";
import { IRatingFilter } from "src/app/modules/rating/rating-page/IRatingFilter";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
})

export class RatingService {
    public routePrefix = '/rating'

    constructor(private httpService: HttpService) {}

    public getEmployeesCounts(month: number) {
        return this.httpService.get<number>(`${this.routePrefix}/${month}`);
    }

    public getEmployees(filter: IRatingFilter) : Observable<IEmployeeQuantity[]> {
        return this.httpService.post(`${this.routePrefix}`, filter)
    }
}
