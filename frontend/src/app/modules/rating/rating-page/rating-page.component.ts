import { Component, OnInit } from '@angular/core';
import { monthSample } from '../../history/history-page/history-page.util';
import { IEmployeeQuantity } from 'src/app/models/IEmployeeQuantity';
import { RatingService } from 'src/app/core/services/rating.service';
import { IMainFilter } from '../../../shared/models/IMainFilter';

@Component({
  selector: 'app-rating-page',
  templateUrl: './rating-page.component.html',
  styleUrls: ['./rating-page.component.sass']
})
export class RatingPageComponent implements OnInit {
  months: string[] =  monthSample;
  employees: IEmployeeQuantity[] = []
  employeesCount: number = 0
  filter: IMainFilter = { pageNumber: 1, pageSize: 15, month: new Date().getMonth() + 1 }
  selectedValue: string = this.months[this.filter.month - 1]

  constructor(private ratingService: RatingService) { }

  ngOnInit(): void {
    this.getEmployeesCount()
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
      this.employees = data
    }))
  }

  getEmployeesCount() {
    this.ratingService.getEmployeesCounts(this.filter.month).subscribe((data) => {
      this.employeesCount = data
    })
  }

  changeMonth(value: string) {
    this.filter.pageNumber = 1
    this.filter.month = monthSample.indexOf(value) + 1
    this.getEmployeesCount()
    this.getEmployeesList()
  }
}
