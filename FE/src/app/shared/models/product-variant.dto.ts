export interface ProductVariantDto {
  attribute_names: Array<string>;
  sku: string;
  quantity: number;
  price: number;
  thumbnail_picture: string;
}

export interface ProductVariantRequestDto {
  attributeNames: Array<string>;
  sKU: string;
  quantity: number;
  price: number;
  thumbnailPicture: string;
}
