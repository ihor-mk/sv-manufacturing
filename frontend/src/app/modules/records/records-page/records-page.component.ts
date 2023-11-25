import { Component, OnInit } from '@angular/core';
import { RecordsService } from 'src/app/core/services/records.service';
import { IGroupRecords } from 'src/app/models/IGroupRecords';
import { ILineProductivityTop } from 'src/app/models/ILineProductivityTop';
import { ILineRecords } from 'src/app/models/ILineRecords';
import { INomenclatureQuantity } from 'src/app/models/INomenclatureQuantity';
import { ITeamQuantity } from 'src/app/models/ITeamQuantity';

@Component({
  selector: 'app-records-page',
  templateUrl: './records-page.component.html',
  styleUrls: ['./records-page.component.sass']
})
export class RecordsPageComponent implements OnInit {

  lineProductivityTop!: ILineProductivityTop[];
  nomenclaturesTop!: INomenclatureQuantity[];
  teamsTop!: ITeamQuantity[];

  constructor(private recordService: RecordsService) { }

  ngOnInit(): void {
    this.recordService.getLineProductivityRating().subscribe((data) =>
      this.lineProductivityTop = data
    )

    this.recordService.getNomenclaturesRating().subscribe((data) =>
      this.nomenclaturesTop = data
    )

    this.recordService.getTeamRating().subscribe((data) => 
    this.teamsTop = data
    )
  }
}
