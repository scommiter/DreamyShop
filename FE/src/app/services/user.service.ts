import { HttpClient } from '@angular/common/http';
import { EnvironmentUrlService } from './environment-url.service';
import { Injectable } from '@angular/core';
import { UserDto, UserUpdateDto } from '../shared/models/user-dto';
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

  currentUser: UserDto = {
    id: 0,
    fullName: '',
    genderType: true,
    dob: new Date(),
    avatar: '',
    email: '',
    phone: '',
    address: '',
    roleTypes: [],
  };
  setUser(user: UserDto) {
    this.currentUser = user;
  }
  getUser() {
    return this.currentUser;
  }

  public assignRole(userId: number, roleIds: number[]) {
    const url = `${environment.apiUrl}/api/Role/assignRole/${userId}`;
    return this.http.post<any>(url, roleIds);
  }
  public updateRole(userId: number, roleIds: number[]) {
    const url = `${environment.apiUrl}/api/Role/updateRole/${userId}`;
    return this.http.put<any>(url, roleIds);
  }

  public updateUser(userId: number, userUpdateDto: UserUpdateDto) {
    const url = `${environment.apiUrl}/api/User/update/${userId}`;
    return this.http.put<any>(url, userUpdateDto);
  }
}
