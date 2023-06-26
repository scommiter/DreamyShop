import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { PaginatorModule } from 'primeng/paginator';
@NgModule({
  declarations: [],
  imports: [CommonModule, ShopRoutingModule, PaginatorModule],
})
export class ShopModule {}
