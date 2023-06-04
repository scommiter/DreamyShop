import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { ProductDto } from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class UpdateProductComponent implements OnInit {
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
  items: MenuItem[] = [];
  home: MenuItem = {};
  productTypes: string[] = [];
  stateOptions: string[] = [];
  isActiveProduct: string = 'False';
  isVisibilityProduct: string = 'False';
  imageCount: number = 0;
  images: File[] = [];
  imageProducts: string[] = [];
  url: string[] = [''];
  isAddVisibility: boolean = true;
  skuProduct: string = '';
  priceProduct: number = 0;
  quantityProduct: number = 0;

  constructor(public productService: ProductService) {}

  ngOnInit(): void {
    this.productUpdate = this.productService.getProductUpdate();
    this.productTypes = [
      'Single',
      'Grouped',
      'Configurable',
      'Bundle',
      'Virtual',
      'Downloadable',
    ];
    this.stateOptions = ['True', 'False'];
    this.productUpdate.productType = Object.values(ProductTypes)[
      this.productUpdate.productType - 1
    ] as ProductTypes;
    this.productUpdate.isActive == true
      ? (this.isActiveProduct = 'True')
      : 'False';
    this.productUpdate.isVisibility == true
      ? (this.isVisibilityProduct = 'True')
      : 'False';
    this.imageCount = this.productUpdate.thumbnailPictures.length;
    this.imageProducts = this.productUpdate.thumbnailPictures;
    this.productUpdate.productAttributeDisplayDtos.length > 0
      ? (this.isAddVisibility = false)
      : true;
    console.log('this.productUpdate :>> ', this.productUpdate);
  }

  updateProduct() {}

  closeImage(index: number) {
    this.imageProducts.splice(index, 1);
    this.images.splice(index, 1);
    this.imageCount--;
  }

  onSelectFile(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      let cacheNumberImage = this.imageCount;
      const files = Array.from(event.target.files).slice(
        0,
        5 - this.images.length
      ) as File[];
      files.forEach((file) => this.images.push(file));
      for (let i = 0; i < event.target.files.length; i++) {
        if (i < 5 - cacheNumberImage) {
          this.imageCount++;
          const file = event.target.files[i];
          const reader = new FileReader();
          reader.readAsDataURL(file); // read file as data url

          reader.onload = () => {
            // called once readAsDataURL is completed
            const imageDataUrl = reader.result as string;
            this.imageProducts.push(imageDataUrl);
            this.url.push(imageDataUrl);
          };
        }
      }
    }
  }

  addProductOptions() {}
}
