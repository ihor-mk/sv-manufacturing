import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainPageComponent } from './main-page/main-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MainRoutingModule } from './main-routing.module';
import { LiveVeiwModule } from '../live-veiw/live-veiw.module';



@NgModule({
  declarations: [
    MainPageComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    SharedModule,
    LiveVeiwModule
  ]
})
export class MainModule { }
