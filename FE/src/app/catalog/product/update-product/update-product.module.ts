import { UpdateProductRoutingModule } from './update-product-routing.module';
import { NgModule } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { CheckboxModule } from 'primeng/checkbox';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FormsModule } from '@angular/forms';
import { EditorModule } from 'primeng/editor';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SplitterModule } from 'primeng/splitter';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogService } from 'primeng/dynamicdialog';
import { DividerModule } from 'primeng/divider';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { PanelModule } from 'primeng/panel';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@NgModule({
  declarations: [],
  imports: [
    FormsModule,
    CommonModule,
    UpdateProductRoutingModule,
    InputTextModule,
    CheckboxModule,
    RadioButtonModule,
    PanelModule,
    FileUploadModule,
    DropdownModule,
    SelectButtonModule,
    EditorModule,
    BreadcrumbModule,
    SplitterModule,
    DialogModule,
    ConfirmDialogModule,
    DividerModule,
    ButtonModule,
    ToastModule,
  ],
  providers: [DialogService, MessageService],
})
export class UpdateProductModule {}
