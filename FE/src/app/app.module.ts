import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './catalog/home/home.component';
import { AdminPageComponent } from './catalog/admin-page/admin-page.component';

@NgModule({
  declarations: [AppComponent, HomeComponent, AdminPageComponent],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule, RouterModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
