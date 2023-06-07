import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { ProductDto } from '../shared/models/product.dto';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { ProductCreateDto } from '../shared/models/product-create-update.dto';
import { PageResultDto } from '../shared/models/page-result.dto';
import { ProductTypes } from '../shared/enums/product-types.enum';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) {}

  public getProducts(
    maxResultCount: number,
    currentPage: number
  ): Observable<PageResultDto<ProductDto>> {
    return this.http.get<PageResultDto<ProductDto>>(
      `${environment.apiUrl}/api/Product/getAll?Page=${currentPage}&Limit=${maxResultCount}`
    );
  }

  public createProduct(productCreateDto: ProductCreateDto): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/Product/create`,
      productCreateDto
    );
  }

  public updateProduct(id: number, productUpdateDto: ProductCreateDto) {
    const url = `${environment.apiUrl}/api/Product/${id}`;
    console.log('Helllo :>> ', url);
    return this.http.put<any>(url, productUpdateDto).subscribe((data) => {});
  }

  public createImageProduct(formData: FormData): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/Product/uploadMultipleImage`,
      formData
    );
  }

  productUpdate: ProductDto = {
    id: 0,
    name: '',
    code: '',
    thumbnailPictures: [],
    productType: ProductTypes.Single,
    categoryName: '',
    manufacturerName: '',
    description: '',
    isActive: false,
    isVisibility: false,
    optionNames: [],
    productAttributeDisplayDtos: [],
    dateCreated: '',
    dateUpdated: '',
  };

  setProductUpdate(product: ProductDto) {
    this.productUpdate = product;
  }

  getProductUpdate() {
    return this.productUpdate;
  }
}
