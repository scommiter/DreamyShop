import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login-routing.module';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, LoginRoutingModule],
  declarations: [LoginComponent],
  providers: [AuthService],
})
export class LoginModule {}
