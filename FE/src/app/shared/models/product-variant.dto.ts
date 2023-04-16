export interface ProductVariantDto {
  id: string;
  product_id: string;
  sku: string;
  is_visibility: boolean;
  is_active: boolean;
  description: string;
  thumnai_picture: string;
  quantity: string;
  price: number;
}
