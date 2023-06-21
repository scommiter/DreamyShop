import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppLayoutModule } from './layout/app.layout.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
import { EditorModule } from 'primeng/editor';
import { NgApexchartsModule } from 'ng-apexcharts';
import { AdminDashBoardComponent } from './catalog/admin-dash-board/admin-dash-board.component';
import { StatisticalPriceComponent } from './catalog/statistical/statistical-price/statistical-price.component';
import { StatisticalAccessComponent } from './catalog/statistical/statistical-access/statistical-access.component';
import { UserComponent } from './catalog/user/user.component';
import { InventoryComponent } from './catalog/ware-house/inventory/inventory.component';
import { OrderComponent } from './catalog/order/order.component';
import { WarehouseComponent } from './catalog/ware-house/warehouse/warehouse.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminDashBoardComponent,
    StatisticalPriceComponent,
    StatisticalAccessComponent,
    UserComponent,
    WarehouseComponent,
    InventoryComponent,
    OrderComponent,
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
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
