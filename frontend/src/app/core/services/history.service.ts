import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { IDoneTask } from "src/app/models/IDoneTask";
import { IMainFilter } from "src/app/shared/models/IMainFilter";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
})

export class HistoryService {
    public routePrefix = '/history'

    constructor(private httpService: HttpService) {}

    public getDoneTasksCounts(month: number) {
        return this.httpService.get<number>(`${this.routePrefix}/${month}`);
    }

    public getDoneTasks(filter: IMainFilter) : Observable<IDoneTask[]> {
        return this.httpService.post(`${this.routePrefix}`, filter)
    }
}