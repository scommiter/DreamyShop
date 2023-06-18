import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './layout/app.layout.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./catalog/authentication/login/login.module').then(
        (m) => m.LoginModule
      ),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./catalog/authentication/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'admin/dash-board',
    pathMatch: 'full',
    loadChildren: () =>
      import('./catalog/admin-dash-board/admin-dash-board-routing.module').then(
        (m) => m.AdminDashBoardRoutingModule
      ),
    component: AppLayoutComponent,
  },
  {
    path: 'product',
    pathMatch: 'full',
    loadChildren: () =>
      import('./catalog/product/product.module').then((m) => m.ProductModule),
    component: AppLayoutComponent,
  },
  {
    path: 'product/create',
    pathMatch: 'full',
    loadChildren: () =>
      import('./catalog/product/product-create/product-create.module').then(
        (m) => m.ProductCreateModule
      ),
  },
  {
    path: 'statistical/sales-volume',
    pathMatch: 'full',
    loadChildren: () =>
      import(
        './catalog/statistical/statistical-price/statistical-price.module'
      ).then((m) => m.StatisticalPriceModule),
    component: AppLayoutComponent,
  },
  {
    path: 'statistical/access-volume',
    pathMatch: 'full',
    loadChildren: () =>
      import(
        './catalog/statistical/statistical-access/statistical-access.module'
      ).then((m) => m.StatisticalAccessModule),
    component: AppLayoutComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
