import { Component, Input, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification.service';
import jwt_decode from "jwt-decode";
import { UserDto } from 'src/app/shared/models/user-dto';

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
export class ShopLayoutComponent implements OnInit{
  countCart: number = 0;
  userName: string = '';

  constructor(private notificationService: NotificationService){}

  ngOnInit(){
     this.notificationService.addToCartAction$.subscribe(() => {
      this.countCart++;
    });
    this.getUserInfo();
  }

  decodedToken: { [key: string]: string; } = {['']: ''};
  getUserInfo(){
    const token = localStorage.getItem('TOKEN');
    const secret = 'DREAMY_SHOP_ACCESS_TOKEN_KEY_ENCRYPTION';
    this.decodedToken = jwt_decode(token as string);
    this.userName = this.decodedToken['FullName']
  }
}
