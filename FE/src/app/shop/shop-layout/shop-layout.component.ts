import { Component, Input, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-shop-layout',
  templateUrl: './shop-layout.component.html',
  styleUrls: ['./shop-layout.component.scss']
})
export class ShopLayoutComponent implements OnInit{
  countCart: number = 0;

  constructor(private notificationService: NotificationService){}

  ngOnInit(): void {
     this.notificationService.addToCartAction$.subscribe(() => {
      this.countCart++;
    });
  }
}
