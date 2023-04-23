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

@NgModule({
  declarations: [CreateProductComponent],
  imports: [
    CommonModule,
    CreateProductRoutingModule,
    InputTextModule,
    CheckboxModule,
    RadioButtonModule,
    PanelModule,
    FileUploadModule,
    DropdownModule,
  ],
})
export class CreateProductModule {}
