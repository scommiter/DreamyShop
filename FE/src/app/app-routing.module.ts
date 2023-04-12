import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './catalog/home/home.component';
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
    path: 'admin',
    pathMatch: 'full',
    loadChildren: () =>
      import('./catalog/admin-page/admin-page-routing.module').then(
        (m) => m.AdminPageRoutingModule
      ),
    component: AppLayoutComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
