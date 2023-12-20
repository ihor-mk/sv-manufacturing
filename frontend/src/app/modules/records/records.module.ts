import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecordsPageComponent } from './records-page/records-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RecordsRoutingModule } from './records-routing.module';
import { MainModule } from '../main/main.module';
import { RecordLineComponent } from './record-line/record-line.component';



@NgModule({
  declarations: [
    RecordsPageComponent,
    RecordLineComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RecordsRoutingModule,
    MainModule
  ]
})
export class RecordsModule { }
