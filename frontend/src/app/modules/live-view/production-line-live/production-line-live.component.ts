import { Component, Input, OnInit } from '@angular/core';
import { ILine } from 'src/app/models/ILine';
import { ILiveViewCounts } from 'src/app/models/ILiveViewCounts';

@Component({
  selector: 'app-production-line-live',
  templateUrl: './production-line-live.component.html',
  styleUrls: ['./production-line-live.component.sass']
})
export class ProductionLineLiveComponent{

  @Input() lineLiveViewCounts!: ILiveViewCounts

  ngOnInit(): void {
    if (!this.lineLiveViewCounts) {
      console.log("EMPTY")
    }
  }
}
