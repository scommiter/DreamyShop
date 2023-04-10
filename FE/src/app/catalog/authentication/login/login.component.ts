import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { TOKEN } from 'src/app/shared/constants/keys.const';
import { LoginRequestDto } from 'src/app/shared/models/login-request.dto';
import { LoginResponseDto } from 'src/app/shared/models/login-response.dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnDestroy {
  private ngUnsubsribe = new Subject<void>();
  loginForm: FormGroup;

  errorMessage: string = '';
  showError: boolean = false;
  show: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.loginForm = this.fb.group({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  password() {
    this.show = !this.show;
  }

  loginUser = (loginFormValue: FormGroup) => {
    this.showError = false;
    const login = { ...loginFormValue };

    const userForAuth: LoginRequestDto = {
      email: login.value.email,
      password: login.value.password,
    };

    //next là một callback function sẽ được gọi khi Observable phát ra một giá trị mới.
    this.authService
      .loginUser('api/auth/login', userForAuth)
      .pipe(takeUntil(this.ngUnsubsribe))
      .subscribe({
        next: (res: LoginResponseDto) => {
          localStorage.setItem(TOKEN, res.token);
          this.authService.sendAuthStateChangeNotification(
            res.isAuthSuccessful
          );
          this.router.navigate(['']);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        },
      });
  };

  ngOnDestroy(): void {
    this.ngUnsubsribe.next();
    this.ngUnsubsribe.complete();
  }
}
