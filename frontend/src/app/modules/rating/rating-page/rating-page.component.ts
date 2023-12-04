import { Component, OnInit } from '@angular/core';
import { monthSample } from '../../history/history-page/history-page.util';
import { IEmployeeQuantity } from 'src/app/models/IEmployeeQuantity';
import { RatingService } from 'src/app/core/services/rating.service';
import { IRatingFilter } from './IRatingFilter';

@Component({
  selector: 'app-rating-page',
  templateUrl: './rating-page.component.html',
  styleUrls: ['./rating-page.component.sass']
})
export class RatingPageComponent implements OnInit {
  months: string[] = []
  selectedValue: string = ""
  employees: IEmployeeQuantity[] = [{ fullName: "Ihor Myroniuk", quantity: 100 },]
  pageNumber: number = 2;
  employeesCount: number = 112
  pageSize: number = 10
  filter: IRatingFilter = { pageNumber: 1, pageSize: 10, month: -1 }

  constructor(private ratingService: RatingService) { }

  ngOnInit(): void {
    this.months = monthSample;
    this.selectedValue = this.months[this.months.length - 1];

    this.ratingService.getEmployeesCounts(this.filter.month).subscribe((data) => {
      this.employeesCount = data
    })

    this.getEmployeesList()
  }

  isPaginNext(): boolean {
    if ((this.employeesCount / this.filter.pageSize) > this.filter.pageNumber)
      return false

    return true
  }

  isPaginPrevious(): boolean {
    if (this.filter.pageNumber > 1)
      return false

    return true
  }
  openPreviousPage() {
    this.filter.pageNumber--
    this.getEmployeesList()
  }

  openNextPage() {
    this.filter.pageNumber++
    this.getEmployeesList()
  }

  getEmployeesList() {
    this.ratingService.getEmployees(this.filter).subscribe((data => {
      this.employees = []
      this.employees = data
    }))
  }
}
