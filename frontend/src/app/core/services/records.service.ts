import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { ILineProductivityTop } from "src/app/models/ILineProductivityTop";
import { INomenclatureQuantity } from "src/app/models/INomenclatureQuantity";

@Injectable({
    providedIn: 'root',
})

export class RecordsService {
    public routePrefix = '/records'

    constructor(private httpService: HttpService) {}

    public getLineProductivityTop() {
        return this.httpService.get<ILineProductivityTop[]>(`${this.routePrefix}/lineProductivity`);
    }

    public getNomenclaturesRating() {
        return this.httpService.get<INomenclatureQuantity[]>(`${this.routePrefix}/nomenclaturesRating`);
    }
}
