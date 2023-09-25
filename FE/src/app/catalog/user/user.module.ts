import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { PanelModule } from 'primeng/panel';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { TabMenuModule } from 'primeng/tabmenu';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FileUploadModule } from 'primeng/fileupload';
import { PaginatorModule } from 'primeng/paginator';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SplitterModule } from 'primeng/splitter';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DividerModule } from 'primeng/divider';
import { ToastModule } from 'primeng/toast';
import { AssignRoleComponent } from './assign-role/assign-role.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserCreateComponent } from './user-create/user-create.component';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { InputMaskModule } from 'primeng/inputmask';
import { HasRoleDirective } from 'src/app/shared/directives/has-role.directive';


@NgModule({
  declarations: [
    UserComponent,
    AssignRoleComponent,
    UserEditComponent,
    UserCreateComponent,
    HasRoleDirective
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UserRoutingModule,
    PanelModule,
    DropdownModule,
    TableModule,
    TabMenuModule,
    ButtonModule,
    FormsModule,
    FileUploadModule,
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
    ToastModule,
    InputMaskModule,
  ],
  providers: [DialogService, MessageService, ConfirmationService],
})
export class UserModule {}
