import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { Observable } from 'rxjs';
import { CartAddDto, CartItemsDto } from '../shared/models/cart-dto';
import { environment } from '../environments/environment';
import { PageResultDto } from '../shared/models/page-result.dto';
import { PagingRequest } from '../shared/models/common-dto';
@Injectable({
  providedIn: 'root',
})
export class CartService  {
  cart: any[] = [];
  productId: number = 0;

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) {}

  addToCartNoLogin(cartItem: any) {
    this.cart.push(cartItem);
  }

  setProductId(id: number){
    this.productId = id;
  }

  public addToCart(cartAddDto: CartAddDto): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/Cart/addToCart`,
      cartAddDto
    );
  }

  public getAddCart(pagingRequest: PagingRequest, userId: number)
  : Observable<PageResultDto<CartItemsDto>> {
    const url = `${environment.apiUrl}/api/Cart/getAll/${userId}`;
    return this.http.put<any>(url, pagingRequest);
  }
}
