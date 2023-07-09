import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { MessageService } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';
import { CartService } from 'src/app/services/cart.service';
import { NotificationService } from 'src/app/services/notification.service';
import { ProductService } from 'src/app/services/product.service';
import { PageResultDto } from 'src/app/shared/models/page-result.dto';
import { ProductDisplayDto } from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent implements OnInit {
  private ngUnsubscribe = new Subject<void>();
  products: ProductDisplayDto[] = [];
  constructor(
    private productService: ProductService,  
    private cartService: CartService,
    private notificationService: NotificationService, 
    private messageService: MessageService,
    private router: Router) 
    {}

  ngOnInit(): void {
    this.getAllProducts();
  }

  getAllProducts() {
    this.productService
      .getProductDisplayShops(8, 1)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PageResultDto<ProductDisplayDto>) => {
          this.products = response.items;
        },
        error: () => {},
      });
  }

  carouselOptions: OwlOptions = {
    loop: true,
    mouseDrag: false,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 600,
    navText: ['&#8249', '&#8250;'],
    responsive: {
      0: {
        items: 1,
      },
      400: {
        items: 2,
      },
      760: {
        items: 3,
      },
      1000: {
        items: 4,
      },
    },
    nav: true,
  };

  carouselItems = [
    {
      imageSrc: '../../../assets/img/carousel-1.jpg',
      title: 'Men Fashion',
      description: 'Thời trang cao cấp, lịch lãm, chuẩn men',
    },
    {
      imageSrc: '../../../assets/img/carousel-2.jpg',
      title: 'Women Fashion',
      description: 'Quý phái, cá tính',
    },
  ];

  //CART
  addToCart(product: any) {
    console.log('Hellu :>> ');
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
}
