import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { ProductDto } from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-variation-update-item',
  templateUrl: './variation-update-item.component.html',
  styleUrls: ['./variation-update-item.component.scss'],
})
export class VariationUpdateItemComponent implements OnInit {
  productUpdate: ProductDto = {
    id: '',
    name: '',
    code: '',
    thumbnailPictures: [],
    productType: ProductTypes.Single,
    categoryName: '',
    manufacturerName: '',
    description: '',
    isActive: false,
    isVisibility: false,
    optionNames: [],
    productAttributeDisplayDtos: [],
    dateCreated: '',
    dateUpdated: '',
  };
  productOptions: { key: string; value: string[] }[] = [
    { key: 'Màu sắc', value: ['Red', 'Blue', ''] },
  ];

  constructor(public productService: ProductService) {}

  ngOnInit(): void {
    this.productUpdate = this.productService.getProductUpdate();
  }

  //trackby return index of of the element inside the loop to determine the change
  trackByFn(index: number, item: { key: string; value: string[] }) {
    return index;
  }
  trackByFnNested(index: number, item: any) {
    return index;
  }
}
