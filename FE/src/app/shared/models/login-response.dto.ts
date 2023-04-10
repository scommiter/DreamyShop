import { AuthEntity } from '../entities/auth-entity';

export interface LoginResponseDto {
  token: string;
  isAuthSuccessful: boolean;
  authen_entity: AuthEntity;
}
