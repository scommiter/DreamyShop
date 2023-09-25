import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppLayoutModule } from './layout/app.layout.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
import { EditorModule } from 'primeng/editor';
import { NgApexchartsModule } from 'ng-apexcharts';
import { AdminDashBoardComponent } from './catalog/admin-dash-board/admin-dash-board.component';
import { StatisticalPriceComponent } from './catalog/statistical/statistical-price/statistical-price.component';
import { StatisticalAccessComponent } from './catalog/statistical/statistical-access/statistical-access.component';
import { OrderComponent } from './catalog/order/order.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { PaginatorModule } from 'primeng/paginator';
import { ProductDetailComponent } from './shop/product-detail/product-detail.component';
import { CartComponent } from './catalog/cart/cart.component';
import { ShopLayoutComponent } from './shop/shop-layout/shop-layout.component';
import { TokenInterceptor } from './shared/http/token.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    AdminDashBoardComponent,
    StatisticalPriceComponent,
    StatisticalAccessComponent,
    OrderComponent,
    ProductDetailComponent,
    CartComponent,
    ShopLayoutComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    RouterModule,
    AppLayoutModule,
    BrowserAnimationsModule,
    DropdownModule,
    FormsModule,
    EditorModule,
    NgApexchartsModule,
    CarouselModule,
    PaginatorModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
