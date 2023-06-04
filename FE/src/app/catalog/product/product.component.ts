import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { ProductService } from 'src/app/services/product.service';
import { ProductDto } from 'src/app/shared/models/product.dto';
import { DialogService } from 'primeng/dynamicdialog';
import { CreateProductComponent } from './create-product/create-product.component';
import { Router } from '@angular/router';
import { PageResultDto } from 'src/app/shared/models/page-result.dto';
import { Subject, takeUntil } from 'rxjs';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit, OnDestroy {
  items: MenuItem[] = [];
  activeItem!: MenuItem;
  products: ProductDto[] = [];
  private ngUnsubscribe = new Subject<void>();

  constructor(
    private productService: ProductService,
    private dialogService: DialogService,
    private router: Router
  ) {}

  ngOnInit() {
    this.items = [
      { label: 'Tất cả', icon: 'pi pi-fw pi-home' },
      { label: 'Active', icon: 'pi pi-check-circle' },
      { label: 'In Active', icon: 'pi pi-minus-circle' },
      { label: 'Hết hàng', icon: 'pi pi-circle-off' },
      { label: 'Giảm giá', icon: 'fa fa-bullhorn' },
      { label: 'Ẩn', icon: 'fa fa-ban' },
    ];
    this.getAllProducts();
    this.activeItem = this.items[0];
  }

  getAllProducts() {
    this.productService
      .getProducts(this.maxResultCount, this.currentPage)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PageResultDto<ProductDto>) => {
          this.products = response.items;
          this.totalCounts = response.totals;
          console.log('this.products :>> ', this.products);
        },
        error: () => {},
      });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  showAddModal(): void {
    const ref = this.dialogService.open(CreateProductComponent, {
      header: 'Create new product',
      width: '70%',
    });
  }

  redirectToProduct() {
    this.router.navigateByUrl('/product/create');
  }

  //UPDATE PRODUCT
  productUpdate: ProductDto = {
    id: '',
    name: '',
    code: '',
    thumbnail_picture: '',
    product_type: ProductTypes.Single,
    category_name: '',
    manufacturer_name: '',
    description: '',
    is_active: false,
    is_visibility: false,
    option_names: [],
    product_attribute_display: [],
    date_created: '',
    date_updated: '',
  };
  updateProduct(id: string) {
    this.productUpdate = this.getProductById(id) as ProductDto;
    console.log(id);
    console.log('this.productUpdate :>> ', this.productUpdate);
  }

  getProductById(id: string): ProductDto | undefined {
    return this.products.find((product) => product.id === id);
  }

  //PAGING
  totalCounts: number = 0;
  maxResultCount: number = 5;
  currentPage: number = 1;
  //rows: number = this.totalCounts / this.maxResultCount;

  onPageChange(event: any): void {
    this.currentPage = event.page + 1;
    this.maxResultCount = event.rows;
    this.getAllProducts();
  }
}
