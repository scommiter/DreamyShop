import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelModule } from 'primeng/panel';
import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { TabMenuModule } from 'primeng/tabmenu';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [ProductComponent],
  imports: [
    CommonModule,
    ProductRoutingModule,
    PanelModule,
    DropdownModule,
    TableModule,
    TabMenuModule,
    ButtonModule,
  ],
})
export class ProductModule {}
