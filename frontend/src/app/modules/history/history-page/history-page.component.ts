import { Component, OnInit } from '@angular/core';
import { monthSample } from './history-page.util';

@Component({
  selector: 'app-history-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.sass']
})
export class HistoryPageComponent implements OnInit{

  months: string[] = []
  selectedValue: string = ""

  ngOnInit(): void {
    this.months = monthSample;
    this.selectedValue = this.months[this.months.length - 1];
  }

}
