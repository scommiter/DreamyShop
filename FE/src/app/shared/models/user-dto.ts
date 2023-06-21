import { RoleTypes } from '../enums/role-types';

export interface UserDto {
  fullName: string;
  genderType: boolean;
  dob: Date;
  avatar: string;
  email: string;
  phone: string;
  address: string;
  roleTypes: number[];
}

export interface UserDisplayDto {
  fullName: string;
  genderType: boolean;
  dob: Date;
  avatar: string;
  email: string;
  phone: string;
  address: string;
  roleTypes: string[];
}
