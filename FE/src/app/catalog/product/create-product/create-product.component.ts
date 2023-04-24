import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class CreateProductComponent implements OnInit {
  productTypes: string[] = [];
  stateOptions: string[] = [];
  defaultOptionActive: string = 'True';
  defaultOptionVisibily: string = 'True';
  text: string = 'aaaaaaaa';

  items: MenuItem[] = [];

  home: MenuItem = {};

  ngOnInit(): void {
    this.productTypes = [
      'Single',
      'Grouped',
      'Configurable',
      'Bundle',
      'Virtual',
      'Downloadable',
    ];
    this.stateOptions = ['True', 'False'];
    this.items = [{ label: 'Product' }, { label: 'Create' }];

    this.home = { icon: 'pi pi-home', routerLink: '/' };
  }
}
