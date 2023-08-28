import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryPageComponent } from './history-page/history-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { HistoryRoutingModule } from './history-routing.module';
import { MainModule } from '../main/main.module';



@NgModule({
  declarations: [
    HistoryPageComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    HistoryRoutingModule,
    MainModule
  ]
})
export class HistoryModule { }
