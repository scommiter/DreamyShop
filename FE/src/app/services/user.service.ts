import { HttpClient } from '@angular/common/http';
import { EnvironmentUrlService } from './environment-url.service';
import { Injectable } from '@angular/core';
import { UserDto } from '../shared/models/user-dto';
import { PageResultDto } from '../shared/models/page-result.dto';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) {}

  public getAllUsers(
    maxResultCount: number,
    currentPage: number
  ): Observable<PageResultDto<UserDto>> {
    return this.http.get<PageResultDto<UserDto>>(
      `${environment.apiUrl}/api/User/getAll?Page=${currentPage}&Limit=${maxResultCount}`
    );
  }
}
