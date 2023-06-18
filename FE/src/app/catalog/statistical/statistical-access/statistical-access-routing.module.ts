import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatisticalAccessComponent } from './statistical-access.component';

const routes: Routes = [{ path: '', component: StatisticalAccessComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StatisticalAccessRoutingModule {}
