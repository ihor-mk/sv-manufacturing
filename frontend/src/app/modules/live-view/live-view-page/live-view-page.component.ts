import { Component, OnInit } from '@angular/core';
import { LiveViewHubService } from 'src/app/core/hubs/live-view-hub.service';
import { LiveViewService } from 'src/app/core/services/live-view.service';
import { ILiveViewCounts } from 'src/app/models/ILiveViewCounts';
import { ILiveViewCountsDto } from 'src/app/shared/models/ILineLiveViewCountsDto';

@Component({
  selector: 'app-live-veiw-page',
  templateUrl: './live-view-page.component.html',
  styleUrls: ['./live-view-page.component.sass']
})
export class LiveViewPageComponent implements OnInit {

  linesLiveViewCounts?: ILiveViewCounts[]

  constructor(
    public liveViewHub: LiveViewHubService,
    public liveViewService: LiveViewService
  ) { }

  async ngOnInit(): Promise<void> {
    this.loadCurrentCounts();
    await this.liveViewHub.start();

    this.liveViewHub.listenMessages((msg) => {
      var listenMessages: ILiveViewCountsDto[] = JSON.parse(msg)
      this.linesLiveViewCounts = this.transformedMessage(listenMessages)
      console.log(this.linesLiveViewCounts)
    });
  }

  loadCurrentCounts() {
    this.liveViewService.getLiveViewCounts().subscribe((data: ILiveViewCounts[]) => {
      this.linesLiveViewCounts = data
      console.log(this.linesLiveViewCounts)

    })
  }

  transformedMessage(listenMessages: ILiveViewCountsDto[]) : ILiveViewCounts[] {
    return listenMessages.map((broadcastMessage) => ({
      //...broadcastMessage,
      lineId: broadcastMessage.LineId,
      lineTitle: broadcastMessage.LineTitle,
      productivityCurrent: broadcastMessage.ProductivityCurrent,
      productivityTop: broadcastMessage.ProductivityTop,
      productivityAvg: broadcastMessage.ProductivityAvg,
      nomenclatureTitle: broadcastMessage.NomenclatureTitle,
      quantityPlan: broadcastMessage.QuantityPlan,
      quantityFact: broadcastMessage.QuantityFact,
      startedAt: broadcastMessage.StartedAt,
      finishedAt: broadcastMessage.FinishedAt,
      workTime: broadcastMessage.WorkTime,
      isNewNomenclature: broadcastMessage.IsNewNomenclature,
      isNewPrinterNomenclature: broadcastMessage.IsNewPrinterNomenclature,
    }))
  }
}
