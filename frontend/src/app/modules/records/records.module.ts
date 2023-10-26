import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecordsPageComponent } from './records-page/records-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RecordsRoutingModule } from './records-routing.module';
import { MainModule } from '../main/main.module';



@NgModule({
  declarations: [
    RecordsPageComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RecordsRoutingModule,
    MainModule
  ]
})
export class RecordsModule { }
