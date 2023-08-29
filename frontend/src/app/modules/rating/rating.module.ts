import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RatingPageComponent } from './rating-page/rating-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RatingRoutingModule } from './rating-routing.module';
import { MainModule } from '../main/main.module';



@NgModule({
  declarations: [
    RatingPageComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RatingRoutingModule,
    MainModule
  ]
})
export class RatingModule { }
