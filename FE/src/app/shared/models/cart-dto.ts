export interface CartAddDto {
    userId: number;
    sku: string;
    quantity: number;
  }

  export interface CartItemsDto {
    productName: number;
    productSKU: string;
    quantity: number;
    price: number;
    tax: number;
  }