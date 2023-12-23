import { Component, Input, OnInit } from '@angular/core';
import { ILiveViewCounts } from 'src/app/models/ILiveViewCounts';

@Component({
  selector: 'app-production-line-live',
  templateUrl: './production-line-live.component.html',
  styleUrls: ['./production-line-live.component.sass']
})
export class ProductionLineLiveComponent implements OnInit {

  @Input() lineLiveViewCounts!: ILiveViewCounts

  ngOnInit(): void {
    if (!this.lineLiveViewCounts) {
      console.log("EMPTY")
    }
  }

  toTimeString(totalSeconds: number = 0): string {
    const totalMs = totalSeconds * 1000;
    const result = new Date(totalMs).toISOString().slice(11, 19);

    return result;
  }

 getWorkTime(): string {
  const dateNow = new Date(Date.now()).getTime()
  const startedAt = new Date(this.lineLiveViewCounts.startedAt!).getTime()

  return new Date(dateNow - startedAt).toISOString().slice(11, 19);
 }

  isLineError(): boolean {
    if (this.lineLiveViewCounts.isNewPrinterNomenclature)
      return true
    
    return false
  }
}
