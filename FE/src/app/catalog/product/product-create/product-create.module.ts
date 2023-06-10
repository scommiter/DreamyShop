import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductCreateRoutingModule } from './product-create-routing.module';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { PanelModule } from 'primeng/panel';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditorModule } from 'primeng/editor';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SplitterModule } from 'primeng/splitter';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DividerModule } from 'primeng/divider';
import { ProductCreateComponent } from './product-create.component';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { VariationEditItemComponent } from '../variation-edit-item/variation-edit-item.component';

@NgModule({
  declarations: [ProductCreateComponent, VariationEditItemComponent],
  imports: [
    CommonModule,
    ProductCreateRoutingModule,
    ReactiveFormsModule,
    InputTextModule,
    CheckboxModule,
    DynamicDialogModule,
    RadioButtonModule,
    PanelModule,
    FileUploadModule,
    DropdownModule,
    SelectButtonModule,
    FormsModule,
    EditorModule,
    BreadcrumbModule,
    SplitterModule,
    DialogModule,
    ConfirmDialogModule,
    DividerModule,
  ],
  exports: [VariationEditItemComponent],
})
export class ProductCreateModule {}
