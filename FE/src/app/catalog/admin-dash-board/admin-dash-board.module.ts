import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminDashBoardRoutingModule } from './admin-dash-board-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AdminDashBoardRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class AdminDashBoardModule {}
