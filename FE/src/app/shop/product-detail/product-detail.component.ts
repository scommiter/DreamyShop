import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { ProductDto } from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  priceProductDetail: string = '';
  productDetail: ProductDto = {
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
    optionNames: [],
    productAttributeDisplayDtos: [],
    dateCreated: '',
    dateUpdated: '',
  };

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let productId = 0;
    this.route.params.subscribe((params: Params) => {
      productId = params['id'];
    });
    this.productService
      .getProductById(productId)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ProductDto) => {
          this.productDetail = response;
          this.getRangePriceProduct();

          console.log('this.productDetail :>> ', this.productDetail);
        },
        error: () => {},
      });
  }

  getRangePriceProduct() {
    let prices = this.productDetail.productAttributeDisplayDtos.map(
      (p) => p.price
    );
    console.log('prices :>> ', prices);
    const max = Math.max(...prices);
    const min = Math.min(...prices);
    this.priceProductDetail = max + 'đ' + ' - ' + min + 'đ';
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
