import { Component } from '@angular/core';
import { ILineRecords } from 'src/app/models/ILineRecords';

@Component({
  selector: 'app-records-page',
  templateUrl: './records-page.component.html',
  styleUrls: ['./records-page.component.sass']
})
export class RecordsPageComponent {
  lines?: ILineRecords[] = [{title: "one", productivity: 155},{title: "two", groupNumber: 125},{title: "three", groupDate: new Date()}]
}
