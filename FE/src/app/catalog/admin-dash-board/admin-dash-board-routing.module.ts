import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashBoardComponent } from './admin-dash-board.component';

const routes: Routes = [{ path: '', component: AdminDashBoardComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminDashBoardRoutingModule {}
