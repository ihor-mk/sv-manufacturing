import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { ILineProductivityTop } from "src/app/models/ILineProductivityTop";
import { INomenclatureQuantity } from "src/app/models/INomenclatureQuantity";
import { ITeamQuantity } from "src/app/models/ITeamQuantity";

@Injectable({
    providedIn: 'root',
})

export class RecordsService {
    public routePrefix = '/records'

    constructor(private httpService: HttpService) {}

    public getLineProductivityRating() {
        return this.httpService.get<ILineProductivityTop[]>(`${this.routePrefix}/lineProductivity`);
    }

    public getNomenclaturesRating() {
        return this.httpService.get<INomenclatureQuantity[]>(`${this.routePrefix}/nomenclaturesRating`);
    }

    public getTeamRating() {
        return this.httpService.get<ITeamQuantity[]>(`${this.routePrefix}/teamRating`);
    }
}

