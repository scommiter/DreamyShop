import { Component, OnDestroy, OnInit } from '@angular/core';
import jwt_decode from "jwt-decode";
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { CartAddDto } from 'src/app/shared/models/cart-dto';
import { ProductDetailDto } from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-product-add-to-cart',
  templateUrl: './product-add-to-cart.component.html',
  styleUrls: ['./product-add-to-cart.component.scss']
})
export class ProductAddToCartComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  priceProductDetail: string = '';
  productDetail: ProductDetailDto = {
    id: 0,
    name: '',
    code: '',
    thumbnailPictures: [],
    productType: ProductTypes.Single,
    categoryName: '',
    manufacturerName: '',
    description: '',
    isActive: false,
    isVisibility: false,
    options: new Map<string, string[]>(),
    productAttributeDisplayDtos: [],
  };

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) {}

  ngOnInit(): void {
    this.productService
      .getProductById(this.config.data.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ProductDetailDto) => {
          this.productDetail = response;
          this.getRangePriceProduct();
        },
        error: () => {},
      });
  }

  getRangePriceProduct() {
    let prices = this.productDetail.productAttributeDisplayDtos.map(
      (p) => p.price
    );
    const max = Math.max(...prices);
    const min = Math.min(...prices);
    this.priceProductDetail = max + 'đ' + ' - ' + min + 'đ';
  }

  decodedToken: { [key: string]: string; } = {['']: ''};
  addToCart(productDetail: any) {
    const token = localStorage.getItem('TOKEN');
    this.decodedToken = jwt_decode(token as string);
    const itemCart : CartAddDto = {
      userId: this.decodedToken["UserID"] as unknown as number,
      sku: '',
      quantity: this.quantity
    }
    this.productDetail.productAttributeDisplayDtos.forEach((aName) => {
      if (this.areArraysEqual(aName.attributeNames, this.options) == true) {
        itemCart.sku = aName.sku;
      }
    });
    console.log('itemCart :>> ', itemCart);
    this.cartService
    .addToCart(itemCart)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: () => {
        this.ref.close(this);
      },
      error: () => {},
    });
  }

  selectedOptions: any[] = [];
  options: string[] = [];
  priceVariant: number = 0;
  updateSelectedOptions(optionKey: string, optionValue: string) {
    const selectedOption = [optionKey, optionValue];
    const index = this.selectedOptions.findIndex(
      (option) => option[0] === optionKey
    );

    if (index > -1) {
      this.selectedOptions[index] = selectedOption;
    } else {
      this.selectedOptions.push(selectedOption);
    }
    this.options = this.selectedOptions.map((p) => p[1].trim());

    //check price of variant
    this.productDetail.productAttributeDisplayDtos.forEach((aName) => {
      if (this.areArraysEqual(aName.attributeNames, this.options) == true) {
        this.priceVariant = aName.price;
      }
    });
  }

  areArraysEqual(arr1: string[], arr2: string[]) {
    if (arr1.length !== arr2.length) {
      return false;
    }
    const sortedArr1 = arr1.slice().sort();
    const sortedArr2 = arr2.slice().sort();
    for (let i = 0; i < sortedArr1.length; i++) {
      if (sortedArr1[i].trim() !== sortedArr2[i].trim()) {
        return false;
      }
    }
    return true;
  }

  quantity: number = 1;

  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  increaseQuantity() {
    this.quantity++;
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
