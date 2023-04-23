import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class CreateProductComponent implements OnInit {
  productTypes: string[] = [
    'Single',
    'Grouped',
    'Configurable',
    'Bundle',
    'Virtual',
    'Downloadable',
  ];
  selectedProductType: string = '';

  ngOnInit(): void {
    this.selectedProductType = this.productTypes[0];
  }
}
