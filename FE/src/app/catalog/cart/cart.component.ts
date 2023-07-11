import { Component, OnDestroy, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { PagingRequest } from 'src/app/shared/models/common-dto';
import jwt_decode from "jwt-decode";
import { Subject, takeUntil } from 'rxjs';
import { CartItemsDto } from 'src/app/shared/models/cart-dto';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();
  decodedToken: { [key: string]: string; } = {['']: ''};
  cartItems: CartItemsDto[] = [];
  quantity: number[] = [];
  totalPrice: number = 0;
  
  constructor(private cartService: CartService){}

  ngOnInit(): void {
    this.getAllCart();
  }

  getAllCart(){
    const token = localStorage.getItem('TOKEN');
    this.decodedToken = jwt_decode(token as string);
    const pagingRequest: PagingRequest = {
      limit: 10,
      page: 1
    }
    this.cartService.getAddCart(pagingRequest, this.decodedToken["UserID"] as unknown as number)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (result: any) => {
          this.cartItems = result.items;
          this.quantity = result.items.quantity;
          this.calculateTotalPrice();
          console.log('this.cartItems :>> ', this.cartItems);
        },
        error: () => {},
      });
  }

  calculateTotalPrice(){
    this.totalPrice = this.cartItems.map(c => c.price * c.quantity).reduce((partialSum, c) => partialSum + c, 0);
  }

  decreaseQuantity(index: number) {
    if (this.cartItems[index].quantity > 1) {
      this.cartItems[index].quantity--;
    }
    this.calculateTotalPrice();
  }

  increaseQuantity(index: number) {
    this.cartItems[index].quantity++;
    this.calculateTotalPrice();
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
