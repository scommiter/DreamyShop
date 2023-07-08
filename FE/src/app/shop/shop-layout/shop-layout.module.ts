import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopLayoutRoutingModule } from './shop-layout-routing.module';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { ToastModule } from 'primeng/toast';

@NgModule({
  declarations: [],
  imports: [CommonModule, ShopLayoutRoutingModule, CarouselModule, ToastModule],
})
export class ShopLayoutModule {}
