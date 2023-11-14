import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LiveViewPageComponent } from './live-view-page/live-view-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MainModule } from '../main/main.module';
import { LiveViewRoutingModule } from './live-view-routing.module';
import { ProductionLineLiveComponent } from './production-line-live/production-line-live.component';


@NgModule({
  declarations: [
    LiveViewPageComponent,
    ProductionLineLiveComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    LiveViewRoutingModule,
    MainModule
  ]
})
export class LiveViewModule { }
