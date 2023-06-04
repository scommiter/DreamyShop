import { ProductTypes } from '../enums/product-types.enum';

export interface ProductDto {
  id: string;
  name: string;
  code: string;
  thumbnailPictures: string[];
  productType: ProductTypes;
  categoryName: string;
  manufacturerName: string;
  description: string;
  isActive: boolean;
  isVisibility: boolean;
  optionNames: string[];
  productAttributeDisplayDtos: Array<ProductAttributeDisplayDto>;
  dateCreated: string;
  dateUpdated: string;
}

export interface ProductAttributeDisplayDto {
  attribute_names: Array<string>;
  sku: string;
  quantity: number;
  price: number;
}
