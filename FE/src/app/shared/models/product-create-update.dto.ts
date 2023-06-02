import { ProductTypes } from '../enums/product-types.enum';
import { ProductVariantRequestDto } from './product-variant.dto';

export interface ProductCreateDto {
  name: string;
  code: string;
  productType: ProductTypes;
  categoryName: string;
  manufacturerName: string;
  description: string;
  isActive: boolean;
  isVisibility: boolean;
  productOptions: { [key: string]: string[] };
  variantProducts: ProductVariantRequestDto[];
}
