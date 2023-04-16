import { ProductVariantDto } from './product-variant.dto';
import { ProductTypes } from '../enums/product-types.enum';

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
  product_variants: Array<ProductVariantDto>;
  date_created: string;
  date_updated: string;
}
