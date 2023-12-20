import { Component, Input } from '@angular/core';
import { ILineProductivityTop } from 'src/app/models/ILineProductivityTop';

@Component({
  selector: 'app-record-line',
  templateUrl: './record-line.component.html',
  styleUrls: ['./record-line.component.sass']
})
export class RecordLineComponent {
  @Input() line!: ILineProductivityTop

  showEmployeesList: boolean = false

  changeVisability() {
    this.showEmployeesList = !this.showEmployeesList
  }
}
