import { Component, Input, OnInit } from '@angular/core';
import { IDoneTask } from 'src/app/models/IDoneTask';

@Component({
  selector: 'app-production-line-history',
  templateUrl: './production-line-history.component.html',
  styleUrls: ['./production-line-history.component.sass']
})
export class ProductionLineHistoryComponent implements OnInit {

  @Input() doneTask!: IDoneTask

  productivity!: number

  ngOnInit(): void {
    this.getProductivity()
  }

  getProductivity() {
    return this.doneTask.quantity / ((new Date(this.doneTask.finishedAt).getTime() - new Date(this.doneTask.startedAt).getTime()) / 60000)
  }
}
