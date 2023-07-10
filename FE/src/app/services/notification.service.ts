import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { ProductDisplayDto, ProductDto } from '../shared/models/product.dto';
import { Observable, Subject } from 'rxjs';
import { environment } from '../environments/environment';
@Injectable({
  providedIn: 'root',
})
export class NotificationService  {
    private addToCartSubject = new Subject<void>();

    addToCartAction$ = this.addToCartSubject.asObservable();
  
    notifyAddToCart() {
      console.log("Helooooooooooooooooooooooooooooo")
      this.addToCartSubject.next();
    }
}
