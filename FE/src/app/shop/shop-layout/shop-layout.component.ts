import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification.service';
import jwt_decode from "jwt-decode";
import { CartService } from 'src/app/services/cart.service';
import { PagingRequest } from 'src/app/shared/models/common-dto';
import { Subject, takeUntil } from 'rxjs';

export interface UserToken{
  email: string;
  fullName: string;
  phone: string;
  roleTypes: number[];
  userID: string;
  aud: string;
  exp: number;
  iss: string;
}

@Component({
  selector: 'app-shop-layout',
  templateUrl: './shop-layout.component.html',
  styleUrls: ['./shop-layout.component.scss']
})
export class ShopLayoutComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();
  countCart: number = 0;
  userName: string = '';

  constructor(private notificationService: NotificationService, private cartService: CartService){}

  ngOnInit(){
    const token = localStorage.getItem('TOKEN');
    this.decodedToken = jwt_decode(token as string);
    const pagingRequest: PagingRequest = {
      limit: 10,
      page: 1
    }
    if(token !== ''){
      this.cartService.getAddCart(pagingRequest, this.decodedToken["UserID"] as unknown as number)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (result: any) => {
          this.countCart = result.totals;
        },
        error: () => {},
      });
    }
    
    this.getUserInfo();
     this.notificationService.addToCartAction$.subscribe(() => {
      if(this.userName === ''){
        this.countCart++;
      }else{
        this.cartService.getAddCart(pagingRequest, this.decodedToken["UserID"] as unknown as number)
          .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe({
            next: (result: any) => {
              this.countCart = result.totals;
            },
            error: () => {},
          });
      }
    });
  }

  decodedToken: { [key: string]: string; } = {['']: ''};
  getUserInfo(){
    const token = localStorage.getItem('TOKEN');
    this.decodedToken = jwt_decode(token as string);
    this.userName = this.decodedToken['FullName']
  }

   ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
