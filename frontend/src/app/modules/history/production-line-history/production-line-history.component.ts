import { Component, Input } from '@angular/core';
import { ILineHistory } from 'src/app/models/ILineHistory';

@Component({
  selector: 'app-production-line-history',
  templateUrl: './production-line-history.component.html',
  styleUrls: ['./production-line-history.component.sass']
})
export class ProductionLineHistoryComponent {
@Input() lineHistory! : ILineHistory
}
