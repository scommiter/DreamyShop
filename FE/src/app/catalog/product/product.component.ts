import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit {
  items: MenuItem[] = [];
  activeItem!: MenuItem;
  cars!: Car[];
  car!: Car;

  constructor() {}

  ngOnInit() {
    this.items = [
      { label: 'All', icon: 'pi pi-fw pi-home' },
      { label: 'Active', icon: 'pi pi-check-circle' },
      { label: 'In Active', icon: 'pi pi-minus-circle' },
      { label: 'Out of stock', icon: 'pi pi-circle-off' },
      { label: 'On promotion', icon: 'fa fa-bullhorn' },
      { label: 'Hidden', icon: 'fa fa-ban' },
    ];
    this.cars = [
      { vin: 'vip', year: '1023', brand: 'asdf', color: '12s' },
      { vin: 'vip', year: '1023', brand: 'asdf', color: '12s' },
      { vin: 'vip', year: '1023', brand: 'asdf', color: '12s' },
      { vin: 'vip', year: '1023', brand: 'asdf', color: '12s' },
      { vin: 'vip', year: '1023', brand: 'asdf', color: '12s' },
    ];
    this.activeItem = this.items[0];
  }
}

export interface Car {
  vin: string;
  year: string;
  brand: string;
  color: string;
}
