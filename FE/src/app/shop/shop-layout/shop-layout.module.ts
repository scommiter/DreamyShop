import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopLayoutRoutingModule } from './shop-layout-routing.module';
import { ShopLayoutComponent } from './shop-layout.component';

@NgModule({
  declarations: [ShopLayoutComponent],
  imports: [CommonModule, ShopLayoutRoutingModule],
})
export class ShopLayoutModule {}
