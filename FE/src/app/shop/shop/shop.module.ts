import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { PaginatorModule } from 'primeng/paginator';
import { CarouselModule } from 'ngx-owl-carousel-o';
@NgModule({
  declarations: [],
  imports: [CommonModule, ShopRoutingModule, PaginatorModule, CarouselModule],
})
export class ShopModule {}
