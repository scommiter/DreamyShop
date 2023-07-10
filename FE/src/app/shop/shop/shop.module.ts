import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { PaginatorModule } from 'primeng/paginator';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ShopComponent } from './shop.component';
import { MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductAddToCartComponent } from '../product-add-to-cart/product-add-to-cart.component';
@NgModule({
  declarations: [ShopComponent, ProductAddToCartComponent],
  imports: [
    CommonModule,
    ShopRoutingModule,
    PaginatorModule,
    CarouselModule,
    ToastModule,
    MatDialogModule,
  ],
  providers: [
    DialogService,
    MessageService,
    ConfirmationService,
  ],
})
export class ShopModule {}
