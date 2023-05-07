import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { map } from 'rxjs/operators';

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
  items: MenuItem[] = [];
  home: MenuItem = {};
  productOptions = new Map<string, string[]>();
  isAddVisibility: boolean = false;

  constructor(private dialogService: DialogService) {}

  ngOnInit(): void {
    this.home = { icon: 'pi pi-home', routerLink: '/' };
    this.items = [{ label: 'Product' }, { label: 'Create' }];
    this.productTypes = [
      'Single',
      'Grouped',
      'Configurable',
      'Bundle',
      'Virtual',
      'Downloadable',
    ];
    this.stateOptions = ['True', 'False'];
  }

  addProductOptions() {
    this.isAddVisibility = false;
  }
}
