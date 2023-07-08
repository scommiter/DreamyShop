import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomePageRoutingModule } from './home-page-routing.module';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { ToastModule } from 'primeng/toast';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { HomePageComponent } from './home-page.component';

@NgModule({
  declarations: [HomePageComponent],
  imports: [CommonModule, HomePageRoutingModule, CarouselModule, ToastModule],
  providers: [DialogService, MessageService, ConfirmationService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class HomePageModule {}
