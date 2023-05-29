import { ProductTypes } from '../enums/product-types.enum';
import { ProductVariantRequestDto } from './product-variant.dto';

export interface ProductCreateDto {
  name: string;
  code: string;
  product_type: ProductTypes;
  category_name: string;
  manufacturer_name: string;
  description: string;
  is_active: boolean;
  is_visibility: boolean;
  product_options: { key: string; value: string[] }[];
  variant_product: Array<ProductVariantRequestDto>;
}
