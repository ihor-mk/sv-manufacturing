import { Injectable } from '@angular/core';
import { SignalRHubFactoryService } from './signalr-hub-factory.service';
import { environment } from 'src/environments/environment';


@Injectable({
    providedIn: 'root',
})
export class LiveViewHubFactoryService extends SignalRHubFactoryService {
    buildUrl(hubUrl: string): string {
        return `${environment.notifierUrl}/${hubUrl}`;
    }
}