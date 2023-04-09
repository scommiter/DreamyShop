import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { LoginRequestDto } from 'src/app/shared/models/login-request.dto';
import { LoginResponseDto } from 'src/app/shared/models/login-response.dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  private returnUrl: string = '';

  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  errorMessage: string = '';
  showError: boolean = false;
  show: boolean = false;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  password() {
    this.show = !this.show;
  }

  loginUser = (loginFormValue: FormGroup) => {
    this.showError = false;
    const login = { ...loginFormValue };

    const userForAuth: LoginRequestDto = {
      username: login.value.username,
      password: login.value.password,
    };

    //next là một callback function sẽ được gọi khi Observable phát ra một giá trị mới.
    this.authService.loginUser('api/auth/login', userForAuth).subscribe({
      next: (res: LoginResponseDto) => {
        localStorage.setItem('access_token', res.access_token);
        this.authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
        this.router.navigate([this.returnUrl]);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
        this.showError = true;
      },
    });
  };
}
