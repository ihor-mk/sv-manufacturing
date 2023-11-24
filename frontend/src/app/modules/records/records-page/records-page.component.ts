import { Component, OnInit } from '@angular/core';
import { RecordsService } from 'src/app/core/services/records.service';
import { IGroupRecords } from 'src/app/models/IGroupRecords';
import { ILineProductivityTop } from 'src/app/models/ILineProductivityTop';
import { ILineRecords } from 'src/app/models/ILineRecords';
import { INomenclatureQuantity } from 'src/app/models/INomenclatureQuantity';

@Component({
  selector: 'app-records-page',
  templateUrl: './records-page.component.html',
  styleUrls: ['./records-page.component.sass']
})
export class RecordsPageComponent implements OnInit {

  lineProductivityTop!: ILineProductivityTop[];
  nomenclaturesRating!: INomenclatureQuantity[];

  constructor(private recordService: RecordsService) { }

  ngOnInit(): void {
    this.recordService.getLineProductivityTop().subscribe((data) =>
      this.lineProductivityTop = data
    )

    this.recordService.getNomenclaturesRating().subscribe((data) =>
      this.nomenclaturesRating = data
    )
  }

  lines?: ILineRecords[] = [{ title: "one", productivity: 155 }, { title: "two", groupNumber: 125 }, { title: "three", groupDate: new Date() }]

  groups?: IGroupRecords[] = [{ title: "Зміна 1" }, { title: "Зміна 1" }, { title: "Зміна 1" }, { title: "Зміна 1" }, { title: "Зміна 1" }, { title: "Зміна 1" }, {}]
}
