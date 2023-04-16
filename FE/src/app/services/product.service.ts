import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { ProductDto } from '../shared/models/product.dto';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) {}

  public getProducts(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(
      `${environment.apiUrl}/api/Product/getAll?Page=1&Limit=10`
    );
  }
}
