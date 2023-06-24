import { RoleTypes } from '../enums/role-types';

export interface UserDto {
  id: number;
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
  genderType: string;
  dob: Date;
  avatar: string;
  email: string;
  phone: string;
  address: string;
  roleTypes: string[];
}

export interface UserUpdateDto {
  fullName: string;
  genderType: boolean;
  dob: Date;
  avatar: string;
  phone: string;
  email: string;
  identityID: string;
  address: string;
  occupation: string;
  country: string;
  profileUrl: string;
}
