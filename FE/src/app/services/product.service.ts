import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { ProductDto } from '../shared/models/product.dto';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { ProductCreateDto } from '../shared/models/product-create-update.dto';

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

  public createProduct(productCreateDto: ProductCreateDto): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/Product/create`,
      productCreateDto
    );
  }

  public createImageProduct(formData: FormData): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/Product/uploadMultipleImage`,
      formData
    );
  }
}
