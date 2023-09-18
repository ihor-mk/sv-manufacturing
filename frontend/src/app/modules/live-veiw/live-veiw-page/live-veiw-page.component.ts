import { Component } from '@angular/core';
import { ILine } from 'src/app/models/ILine';

@Component({
  selector: 'app-live-veiw-page',
  templateUrl: './live-veiw-page.component.html',
  styleUrls: ['./live-veiw-page.component.sass']
})
export class LiveVeiwPageComponent {
  constructor() {
    this.lineArray = [this.lineTemplate, this.lineTemplate, this.lineTemplate, this.lineTemplate, this.lineTemplate]
  }
  lineTemplate : ILine = {
    title : 'ЛІНІЯ 1',
    productivityOnline: 12,
    productivityTop: 15,
    nomenclature: 'NOM-17/23',
    plan: 1000,
    fact: 237,
    progress: 0,
    avgSpeed: 0,
    time: new Date(),
    endTime: new Date()
   }
  
   lineArray : ILine[]
}
