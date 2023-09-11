import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LiveVeiwPageComponent } from './live-veiw-page/live-veiw-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MainModule } from '../main/main.module';
import { LiveVeiwRoutingModule } from './live-veiw-routing.module';
import { ProductionLineLiveComponent } from './production-line-live/production-line-live.component';


@NgModule({
  declarations: [
    LiveVeiwPageComponent,
    ProductionLineLiveComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    LiveVeiwRoutingModule,
    MainModule
  ]
})
export class LiveVeiwModule { }
