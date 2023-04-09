import { AuthEntity } from '../entities/auth-entity';

export interface LoginResponseDto {
  access_token: string;
  isAuthSuccessful: boolean;
  authen_entity: AuthEntity;
}
