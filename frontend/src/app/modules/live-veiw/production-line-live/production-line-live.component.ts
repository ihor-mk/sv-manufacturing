import { Component, Input, OnInit } from '@angular/core';
import { ILine } from 'src/app/models/ILine';

@Component({
  selector: 'app-production-line-live',
  templateUrl: './production-line-live.component.html',
  styleUrls: ['./production-line-live.component.sass']
})
export class ProductionLineLiveComponent implements OnInit {

  @Input() line!: ILine;


  ngOnInit(): void {
    if (!this.line) {
      console.log("EMPTY")
    }
  }
}
