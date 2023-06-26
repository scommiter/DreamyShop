import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';
import { PageResultDto } from 'src/app/shared/models/page-result.dto';
import { ProductDisplayDto } from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  products: ProductDisplayDto[] = [];

  constructor(private productService: ProductService) {}

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
