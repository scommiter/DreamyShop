export interface ProductDto {
  id: string;
  name: string;
  code: string;
  thumbnail_picture: string;
  price: number;
  product_type: ProductTypes;
  category_name: string;
  manufacturer_name: string;
  description: string;
  is_active: boolean;
  is_visibility: boolean;
  date_created: Date;
  date_updated: Date;
}
