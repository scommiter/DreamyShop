import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import {
  ProductVariantDto,
  ProductVariantRequestDto,
} from 'src/app/shared/models/product-variant.dto';
import {
  ProductAttributeDisplayDto,
  ProductDto,
} from 'src/app/shared/models/product.dto';

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
  productOptions: { key: string; value: string[] }[] = [];
  addClassifyProduct: boolean = false;
  productVariantTwos: ProductVariantRequestDto[][] = [];
  isVisibilityTwo: boolean[][] = [];
  checkCountOptionsOne: number = 0;
  checkIsAddVariant: boolean = false;
  imageVariants: File[] = [];

  constructor(public productService: ProductService) {}

  ngOnInit(): void {
    this.productUpdate = this.productService.getProductUpdate();
    this.convertToProductOptions(
      this.productUpdate.optionNames,
      this.productUpdate.productAttributeDisplayDtos
    );
    this.productUpdate.optionNames.length > 1
      ? (this.addClassifyProduct = true)
      : (this.addClassifyProduct = false);
    console.log('this.productUpdate :>> ', this.productUpdate);
    if (this.addClassifyProduct == true) {
      this.convertToProductVariantTwos(
        this.productUpdate.productAttributeDisplayDtos
      );
      console.log('this.productVariantTwos :>> ', this.productVariantTwos);
    }
  }

  convertToProductOptions(
    optionNames: string[],
    productAttributeDisplayDtos: ProductAttributeDisplayDto[]
  ) {
    for (let i = 0; i < optionNames.length; i++) {
      this.productOptions.push({ key: optionNames[i], value: [] });
      for (let j = 0; j < productAttributeDisplayDtos.length; j++) {
        this.productOptions[i].value.push(
          productAttributeDisplayDtos[j].attributeNames[i]
        );
      }
      this.productOptions[i].value = [...new Set(this.productOptions[i].value)];
      this.productOptions[i].value.push('');
    }
  }

  convertProductAttributeDisplayDtos(
    productAttributes: ProductAttributeDisplayDto[]
  ) {
    for (let i = 0; i < this.productOptions[0].value.length - 1; i++) {}
  }

  convertToProductVariantTwos(productAttributes: ProductAttributeDisplayDto[]) {
    let index = 0;
    let productOptionLength = this.productOptions[0].value.length - 1;
    let cache = 0;
    for (let i = 0; i < productOptionLength; i++) {
      cache = i;
      this.productVariantTwos.push([]);
      for (let j = 0; j < this.productOptions[1].value.length - 1; j++) {
        this.productVariantTwos[i].push({
          attributeNames: [''],
          sKU: '',
          quantity: 0,
          price: 0,
          thumbnailPicture: '',
        });
        let productOptionValues: string[] = [];
        productOptionValues.push(this.productOptions[0].value[i]);
        productOptionValues.push(this.productOptions[1].value[j]);
        console.log('productOptionsValues :>> ', productOptionValues);
        this.productVariantTwos[i][j].attributeNames = productOptionValues;
        this.productVariantTwos[i][j].sKU = productAttributes[cache].sku;
        this.productVariantTwos[i][j].quantity =
          productAttributes[cache].quantity;
        this.productVariantTwos[i][j].price = productAttributes[cache].price;
        this.productVariantTwos[i][j].thumbnailPicture =
          productAttributes[cache].image;
        if (cache + productOptionLength < this.productOptions[1].value.length) {
          cache = cache + productOptionLength;
        }
        console.log('productAttributes[cache] :>> ', productAttributes[cache]);
        index++;
      }
      console.log('----------------------------');
    }
  }

  addClassifyProductVariant() {}

  onInputValue() {}

  //upload thumnail Image
  isVisibility: boolean[] = [];
  url: string[] = [''];
  onSelectFile(event: any, indexOption: number) {}
  onSelectFileVariantTwo(
    event: any,
    indexOption1: number,
    indexOption2: number
  ) {}

  //trackby return index of of the element inside the loop to determine the change
  trackByFn(index: number, item: { key: string; value: string[] }) {
    return index;
  }
  trackByFnNested(index: number, item: any) {
    return index;
  }
}
