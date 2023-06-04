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
import { PaginatorModule } from 'primeng/paginator';
import { UpdateProductComponent } from './update-product/update-product.component';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SplitterModule } from 'primeng/splitter';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DividerModule } from 'primeng/divider';
import { VariationUpdateItemComponent } from './variation-update-item/variation-update-item.component';

@NgModule({
  declarations: [ProductComponent, UpdateProductComponent, VariationUpdateItemComponent],
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
    FormsModule,
    PaginatorModule,
    InputTextModule,
    CheckboxModule,
    RadioButtonModule,
    SelectButtonModule,
    BreadcrumbModule,
    SplitterModule,
    DialogModule,
    ConfirmDialogModule,
    DividerModule,
  ],
  providers: [ProductService, DialogService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProductModule {}
