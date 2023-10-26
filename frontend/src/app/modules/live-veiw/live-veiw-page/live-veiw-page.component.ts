import { Component, OnInit } from '@angular/core';
import { LiveViewHubService } from 'src/app/core/hubs/live-veiw-hub.service';
import { ILine } from 'src/app/models/ILine';

@Component({
  selector: 'app-live-veiw-page',
  templateUrl: './live-veiw-page.component.html',
  styleUrls: ['./live-veiw-page.component.sass']
})
export class LiveVeiwPageComponent implements OnInit{
  constructor(
    public liviViewHub: LiveViewHubService
  ) {
    this.lineArray = [this.lineTemplate, this.lineTemplate, this.lineTemplate, this.lineTemplate, this.lineTemplate]
  }
  async ngOnInit(): Promise<void> {
   await this.liviViewHub.start();

   this.liviViewHub.listenMessages((msg) => {
    const broadcastMessage = JSON.parse(msg);

    const messages = {
        id: broadcastMessage.Id,
        currentCount: broadcastMessage.CurrentCount,
    };

    console.log(messages)
    this.lineTemplate.fact = messages.currentCount
});
  }


  lineTemplate : ILine = {
    title : 'ЛІНІЯ 1',
    productivityOnline: 12,
    productivityTop: 15,
    nomenclature: 'NOM-17/23',
    plan: 1000,
    fact: 237,
    progress: 0,
    avgSpeed: 0,
    time: new Date(),
    endTime: new Date()
   }
  
   lineArray : ILine[]
}
