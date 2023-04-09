import { LoginRequestDto } from '../shared/models/login-request.dto';
import { HttpClient } from '@angular/common/http';
import { EnvironmentUrlService } from './environment-url.service';
import { LoginResponseDto } from '../shared/models/login-response.dto';
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) {}

  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();

  public loginUser = (route: string, body: LoginRequestDto) => {
    return this.http.post<LoginResponseDto>(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body
    );
  };

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  };

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  };
}
