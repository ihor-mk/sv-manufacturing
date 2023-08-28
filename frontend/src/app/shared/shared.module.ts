import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { MaterialModule } from './material/material.module';
import { RouterModule } from '@angular/router';



@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  declarations: [
    HeaderComponent
  ],
  
  exports: [
    HeaderComponent,
    MaterialModule,
    RouterModule
]
})
export class SharedModule { }
