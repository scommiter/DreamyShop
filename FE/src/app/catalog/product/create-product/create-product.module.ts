import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateProductRoutingModule } from './create-product-routing.module';
import { CreateProductComponent } from './create-product.component';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { CheckboxModule } from 'primeng/checkbox';
import { PanelModule } from 'primeng/panel';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FormsModule } from '@angular/forms';
import { EditorModule } from 'primeng/editor';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SplitterModule } from 'primeng/splitter';

@NgModule({
  declarations: [CreateProductComponent],
  imports: [
    CreateProductRoutingModule,
    InputTextModule,
    CheckboxModule,
    RadioButtonModule,
    PanelModule,
    FileUploadModule,
    DropdownModule,
    SelectButtonModule,
    FormsModule,
    EditorModule,
    BreadcrumbModule,
    SplitterModule,
  ],
})
export class CreateProductModule {}
