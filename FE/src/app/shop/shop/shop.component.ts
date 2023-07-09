import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';
import { CartService } from 'src/app/services/cart.service';
import { NotificationService } from 'src/app/services/notification.service';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { PageResultDto } from 'src/app/shared/models/page-result.dto';
import {
  ProductDisplayDto,
  ProductDto,
} from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  products: ProductDisplayDto[] = [];
  product: ProductDto = {
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
    private router: Router, 
    private cartService: CartService,
    private notificationService: NotificationService, 
    private messageService: MessageService, ) {}

  ngOnInit(): void {
    this.getAllProducts();
  }

  getAllProducts() {
    this.productService
      .getProductDisplayShops(this.maxResultCount, this.currentPage)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PageResultDto<ProductDisplayDto>) => {
          this.products = response.items;
          this.totalCounts = response.totals;
          console.log('this.products 1111111111111111:>> ', this.products);
        },
        error: () => {},
      });
  }

  redirectToProductDetail(id: number) {
    this.router.navigate(['/shop/product', id]);
  }

  //CART
  addToCart(product: any) {
    this.cartService.addToCart(product);
    this.notificationService.notifyAddToCart();
    this.showCreateSuccess();
  }

  showCreateSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Thành công',
      detail: 'Đã sản phẩm vào giỏ hàng',
    });
  }

  //PAGING
  totalCounts: number = 0;
  maxResultCount: number = 9;
  currentPage: number = 1;
  //rows: number = this.totalCounts / this.maxResultCount;

  onPageChange(event: any): void {
    this.currentPage = event.page + 1;
    this.maxResultCount = event.rows;
    this.getAllProducts();
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
