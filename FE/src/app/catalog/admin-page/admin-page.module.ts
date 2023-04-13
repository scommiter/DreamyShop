import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminPageRoutingModule } from './admin-page-routing.module';
import { AdminPageComponent } from './admin-page.component';

@NgModule({
  declarations: [AdminPageComponent],
  imports: [CommonModule, AdminPageRoutingModule],
})
export class HomeModule {}
