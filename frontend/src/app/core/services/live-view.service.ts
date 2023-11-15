import { ILiveViewCounts } from "src/app/models/ILiveViewCounts";
import { HttpService } from "./http.service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root',
})

export class LiveViewService {
    public routePrefix = '/liveviewcounts'

    constructor(private httpService: HttpService) {}

    public getLiveViewCounts() {
        return this.httpService.get<ILiveViewCounts[]>(`${this.routePrefix}`);
    }
}
