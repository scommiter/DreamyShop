import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { ProductDisplayDto, ProductDto } from '../shared/models/product.dto';
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
  private isProductUpdated: boolean = false;
  private isSuccessCreate: boolean = false;

  public getProducts(
    maxResultCount: number,
    currentPage: number
  ): Observable<PageResultDto<ProductDto>> {
    return this.http.get<PageResultDto<ProductDto>>(
      `${environment.apiUrl}/api/Product/getAll?Page=${currentPage}&Limit=${maxResultCount}`
    );
  }

  public getProductDisplayShops(
    maxResultCount: number,
    currentPage: number
  ): Observable<PageResultDto<ProductDisplayDto>> {
    return this.http.get<PageResultDto<ProductDisplayDto>>(
      `${environment.apiUrl}/api/Product/getAllDisplay?Page=${currentPage}&Limit=${maxResultCount}`
    );
  }

  public SetCreateSuccess(isSuccess: boolean): void {
    this.isSuccessCreate = isSuccess;
    console.log(
      'Linhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh :>> ',
      this.isSuccessCreate
    );
  }
  public CheckCreateSuccess(): boolean {
    console.log('this.isSuccessCreate :>> ', this.isSuccessCreate);
    return this.isSuccessCreate === true;
  }

  public createProduct(productCreateDto: ProductCreateDto): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/Product/create`,
      productCreateDto
    );
  }

  public updateProduct(id: number, productUpdateDto: ProductCreateDto) {
    const url = `${environment.apiUrl}/api/Product/${id}`;
    return this.http.put<any>(url, productUpdateDto);
  }

  public createImageProduct(formData: FormData): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/Product/uploadMultipleImage`,
      formData
    );
  }

  public deleteProduct(id: number): Observable<any> {
    const url = `${environment.apiUrl}/api/Product/${id}`;
    return this.http.delete<any>(url);
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

  setProductUpdate(product: ProductDto, value: boolean) {
    this.productUpdate = product;
    this.isProductUpdated = value;
  }
  getIsProductUpdated() {
    return this.isProductUpdated;
  }
  getProductUpdate() {
    return this.productUpdate;
  }

  //Excell
  downloadReport() {
    const url = `${environment.apiUrl}/api/Report/excell/Export`;
    this.http.get(url, { responseType: 'blob' }).subscribe((response: Blob) => {
      const downloadUrl = URL.createObjectURL(response);
      const link = document.createElement('a');
      link.href = downloadUrl;
      link.download = `ProductReport-${new Date().toLocaleDateString()}.xlsx`; // Đặt tên cho file
      link.click();
    });
  }

  uploadFile(file: File): Observable<any> {
    const url = `${environment.apiUrl}/api/Report/excell/Import`;
    const formData = new FormData();
    formData.append('reportFile', file);
    return this.http.post<any>(url, formData);
  }
}
