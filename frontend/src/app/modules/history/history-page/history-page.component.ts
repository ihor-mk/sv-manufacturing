import { Component, OnInit } from '@angular/core';
import { monthSample } from './history-page.util';
import { ILineHistory } from 'src/app/models/ILineHistory';

@Component({
  selector: 'app-history-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.sass']
})
export class HistoryPageComponent implements OnInit {

  months: string[] = []
  selectedValue: string = ""
  lineTemplate: ILineHistory = { title: "Line 1", productionsCount: 100, productivityAvg: 15, speedTop: 30, speedAvg: 11 }
  lineArray: ILineHistory[] = [this.lineTemplate, this.lineTemplate, this.lineTemplate, this.lineTemplate, this.lineTemplate]

  ngOnInit(): void {
    this.months = monthSample;
    this.selectedValue = this.months[this.months.length - 1];
  }

}
