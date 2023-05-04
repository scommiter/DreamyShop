import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelModule } from 'primeng/panel';
import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { TabMenuModule } from 'primeng/tabmenu';
import { ButtonModule } from 'primeng/button';
import { ProductService } from 'src/app/services/product.service';
import { FormsModule } from '@angular/forms';
import { DialogService } from 'primeng/dynamicdialog';
import { FileUploadModule } from 'primeng/fileupload';
import { EditorModule } from 'primeng/editor';
import { AddProductVariantComponent } from './add-product-variant/add-product-variant.component';

@NgModule({
  declarations: [ProductComponent, AddProductVariantComponent],
  imports: [
    CommonModule,
    ProductRoutingModule,
    PanelModule,
    DropdownModule,
    TableModule,
    TabMenuModule,
    ButtonModule,
    FormsModule,
    FileUploadModule,
    EditorModule,
  ],
  providers: [ProductService, DialogService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProductModule {}
