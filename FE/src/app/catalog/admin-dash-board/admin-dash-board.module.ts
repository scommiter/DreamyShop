import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminDashBoardRoutingModule } from './admin-dash-board-routing.module';
import { AdminDashBoardComponent } from './admin-dash-board.component';

@NgModule({
  declarations: [AdminDashBoardComponent],
  imports: [CommonModule, AdminDashBoardRoutingModule],
})
export class AdminDashBoardModule {}
