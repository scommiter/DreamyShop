import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatisticalPriceComponent } from './statistical-price.component';

const routes: Routes = [{ path: '', component: StatisticalPriceComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StatisticalPriceRoutingModule {}
