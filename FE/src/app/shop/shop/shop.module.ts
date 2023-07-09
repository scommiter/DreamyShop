import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { PaginatorModule } from 'primeng/paginator';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ShopComponent } from './shop.component';
@NgModule({
  declarations: [ShopComponent],
  imports: [CommonModule, ShopRoutingModule, PaginatorModule, CarouselModule, ToastModule],
  providers: [DialogService, MessageService, ConfirmationService],
})
export class ShopModule {}
