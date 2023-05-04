import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { AddProductVariantComponent } from '../add-product-variant/add-product-variant.component';

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
  visible: boolean = false;

  constructor(private dialogService: DialogService) {}

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

  showAddModal(): void {
    const ref = this.dialogService.open(AddProductVariantComponent, {
      header: 'Add Product Variant',
      width: '70%',
    });
  }
}
