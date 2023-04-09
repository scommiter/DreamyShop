export interface AuthEntity {
  // UserID
  // FullName
  // public string Email { get; set; }
  // public string Avatar { get; set; }
  // public string Phone { get; set; }
  // public List<byte> RoleTypes { get; set; }
  user_id: string;
  full_name: string;
  email: string;
  phone: string;
  role_types: Uint8Array[];
}
