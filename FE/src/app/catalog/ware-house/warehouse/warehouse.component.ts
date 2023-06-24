import { Component } from '@angular/core';
import { WarehouseDto } from 'src/app/shared/models/warehouse-dto';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss'],
})
export class WarehouseComponent {
  tickets: WarehouseDto[] = [];
  constructor() {}

  //PAGING
  totalCounts: number = 0;
  maxResultCount: number = 5;
  currentPage: number = 1;

  onPageChange(event: any): void {
    this.currentPage = event.page + 1;
    this.maxResultCount = event.rows;
  }

  saveChange() {}
}
