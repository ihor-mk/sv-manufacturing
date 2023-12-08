import { Component, OnInit } from '@angular/core';
import { monthSample } from './history-page.util';
import { ILineHistory } from 'src/app/models/ILineHistory';
import { HistoryService } from 'src/app/core/services/history.service';
import { IMainFilter } from 'src/app/shared/models/IMainFilter';
import { IDoneTask } from 'src/app/models/IDoneTask';

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

  filter: IMainFilter = { pageNumber: 1, pageSize: 10, month: new Date().getMonth() }
  doneTasksCount: number = 0
  doneTasks: IDoneTask[] = []

  constructor (private historyService: HistoryService){}

  ngOnInit(): void {
    this.months = monthSample;
    this.selectedValue = this.months[this.months.length - 1];
    this.getDoneTasksCount()
    this.getDoneTasksList()
  }

  getDoneTasksList() {
    this.historyService.getDoneTasks(this.filter).subscribe((data => {
      this.doneTasks = data
    }))
  }

  getDoneTasksCount() {
    this.historyService.getDoneTasksCounts(this.filter.month).subscribe((data) => {
      this.doneTasksCount = data
      this.filter.pageNumber = 1
    })
  }

  changeMonth(value: string) {
    this.filter.month = monthSample.indexOf(value) + 1
    this.getDoneTasksCount()
    this.getDoneTasksList()
  }

  isPaginNext(): boolean {
    if ((this.doneTasksCount / this.filter.pageSize) > this.filter.pageNumber)
      return false

    return true
  }

  isPaginPrevious(): boolean {
    if (this.filter.pageNumber > 1)
      return false

    return true
  }
  openPreviousPage() {
    this.filter.pageNumber--
    this.getDoneTasksList()
  }

  openNextPage() {
    this.filter.pageNumber++
    this.getDoneTasksList()
  }
}
