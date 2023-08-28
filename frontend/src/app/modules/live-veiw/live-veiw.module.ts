import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LiveVeiwPageComponent } from './live-veiw-page/live-veiw-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MainModule } from '../main/main.module';
import { LiveVeiwRoutingModule } from './live-veiw-routing.module';


@NgModule({
  declarations: [
    LiveVeiwPageComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    LiveVeiwRoutingModule
  ]
})
export class LiveVeiwModule { }
