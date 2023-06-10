import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  ConfirmEventType,
  ConfirmationService,
  MenuItem,
  MessageService,
} from 'primeng/api';
import { ProductService } from 'src/app/services/product.service';
import { ProductDto } from 'src/app/shared/models/product.dto';
import { DialogService } from 'primeng/dynamicdialog';
import { Router } from '@angular/router';
import { PageResultDto } from 'src/app/shared/models/page-result.dto';
import { Subject, takeUntil } from 'rxjs';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { UpdateProductComponent } from './update-product/update-product.component';
import { ProductCreateDto } from 'src/app/shared/models/product-create-update.dto';

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
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
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
    if (this.productService.CheckCreateSuccess()) {
      setTimeout(() => {
        this.showCreateSuccess();
      }, 500);
    }
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  redirectToProduct() {
    this.router.navigateByUrl('/product/create');
  }

  //UPDATE PRODUCT
  productUpdate: ProductDto = {
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
  updateProduct(id: number) {
    this.productUpdate = this.getProductById(id) as ProductDto;
    this.productService.setProductUpdate(this.productUpdate, false);
    const ref = this.dialogService.open(UpdateProductComponent, {
      header: 'CẬP NHẬT SẢN PHẨM',
      width: '70%',
    });
    ref.onClose.subscribe((data: ProductCreateDto) => {
      if (data) {
        this.getAllProducts();
      }
    });
  }

  showCreateSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Thành công',
      detail: 'Tạo mới sản phẩm thành công',
    });
  }

  deleteProduct(id: number) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc muốn xóa không?',
      header: 'Xác nhận',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.messageService.add({
          severity: 'info',
          summary: 'Xác nhận',
          detail: 'Xóa thành công',
        });
        this.productService.deleteProduct(id).subscribe({
          next: () => {
            this.getAllProducts();
          },
          error: (err) => {},
        });
      },
      reject: (type: any) => {
        this.messageService.add({
          severity: 'warn',
          summary: 'Đã hủy',
          detail: 'Bạn đã hủy xóa',
        });
      },
    });
  }

  getProductById(id: number): ProductDto | undefined {
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
