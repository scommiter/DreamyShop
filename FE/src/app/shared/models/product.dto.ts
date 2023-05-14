import { ProductTypes } from '../enums/product-types.enum';

export interface ProductDto {
  id: string;
  name: string;
  code: string;
  thumbnail_picture: string;
  product_type: ProductTypes;
  category_name: string;
  manufacturer_name: string;
  description: string;
  is_active: boolean;
  is_visibility: boolean;
  product_attribute_display: Array<ProductAttributeDisplayDto>;
  date_created: string;
  date_updated: string;
}

export interface ProductAttributeDisplayDto {
  attribute_names: Array<string>;
  sku: string;
  quantity: number;
  price: number;
}
