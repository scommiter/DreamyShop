export interface AuthEntity {
  user_id: string;
  full_name: string;
  email: string;
  phone: string;
  role_types: Uint8Array[];
}
