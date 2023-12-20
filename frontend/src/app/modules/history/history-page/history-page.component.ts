import { Component, OnInit } from '@angular/core';
import { monthSample } from './history-page.util';
import { HistoryService } from 'src/app/core/services/history.service';
import { IMainFilter } from 'src/app/shared/models/IMainFilter';
import { IDoneTask } from 'src/app/models/IDoneTask';

@Component({
  selector: 'app-history-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.sass']
})
export class HistoryPageComponent implements OnInit {
  months: string[] = monthSample;
  doneTasksCount: number = 0
  doneTasks: IDoneTask[] = []
  filter: IMainFilter = { pageNumber: 1, pageSize: 10, month: new Date().getMonth() + 1 }
  selectedValue = this.months[this.filter.month - 1];

  constructor (private historyService: HistoryService){}

  ngOnInit(): void {
    this.getDoneTasksCount()
    this.getDoneTasksList()
  }

  getDoneTasksCount() {
    this.historyService.getDoneTasksCounts(this.filter.month).subscribe((data) => {
      this.doneTasksCount = data
    })
  }

  getDoneTasksList() {
    this.historyService.getDoneTasks(this.filter).subscribe((data => {
      this.doneTasks = data
    }))
  }
  changeMonth(value: string) {
    this.filter.pageNumber = 1
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
