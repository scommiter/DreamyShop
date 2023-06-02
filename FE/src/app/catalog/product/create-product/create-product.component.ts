import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { ProductCreateDto } from 'src/app/shared/models/product-create-update.dto';
import {
  ProductVariantDto,
  ProductVariantRequestDto,
} from 'src/app/shared/models/product-variant.dto';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class CreateProductComponent implements OnInit {
  productCreateRequest: ProductCreateDto = {
    name: '',
    code: '',
    productType: ProductTypes.Single,
    categoryName: '',
    manufacturerName: '',
    description: '',
    isActive: true,
    isVisibility: true,
    productOptions: {},
    variantProducts: [],
  };
  productTypes: string[] = [];
  stateOptions: string[] = [];
  defaultOptionActive: string = 'True';
  defaultOptionVisibily: string = 'True';
  items: MenuItem[] = [];
  home: MenuItem = {};
  skuProduct: string = '';
  imageProducts: string[] = [];
  url: string[] = [''];
  imageCount: number = 0;
  priceProduct: number = 0;
  quantityProduct: number = 0;
  attributeNames: string[][] = [];
  isAddVisibility: boolean = true;
  images: File[] = [];
  imageVariants: File[] = [];
  constructor(
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.home = { icon: 'pi pi-home', routerLink: '/' };
    this.items = [{ label: 'Product' }, { label: 'Create' }];
    this.productTypes = [
      'Single',
      'Grouped',
      'Configurable',
      'Bundle',
      'Virtual',
      'Downloadable',
    ];
    this.stateOptions = ['True', 'False'];
  }

  addProductOptions() {
    this.isAddVisibility = false;
  }

  onSelectFile(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      let cacheNumberImage = this.images.length;
      const files = Array.from(event.target.files).slice(
        0,
        5 - this.images.length
      ) as File[];
      files.forEach((file) => this.images.push(file));
      for (let i = 0; i < event.target.files.length; i++) {
        if (i < 5 - cacheNumberImage) {
          this.imageCount++;
          const file = event.target.files[i];
          const reader = new FileReader();
          reader.readAsDataURL(file); // read file as data url

          reader.onload = () => {
            // called once readAsDataURL is completed
            const imageDataUrl = reader.result as string;
            this.imageProducts.push(imageDataUrl);
            this.url.push(imageDataUrl);
          };
        }
      }
    }
  }

  // addImage(imageFiles: File[]) {
  //   const formData = new FormData();
  //   imageFiles.forEach((image) => formData.append('image', image));
  //   this.productService.createImageProduct(formData);
  //   // this.http
  //   //   .post(`${environment.apiUrl}/api/Product/uploadMultipleImage`, formData)
  //   //   .subscribe(
  //   //     (response) => {
  //   //       // Xử lý phản hồi từ API (nếu cần)
  //   //       console.log('Image added successfully');
  //   //     },
  //   //     (error) => {
  //   //       // Xử lý lỗi (nếu có)
  //   //       console.error('Failed to add image', error);
  //   //     }
  //   //   );
  // }

  closeImage(index: number) {
    this.imageProducts.splice(index, 1);
    this.images.splice(index, 1);
    this.imageCount--;
  }
  productOptions: { key: string; value: string[] }[] = [];
  productVariants: ProductVariantDto[] = [];
  productVariantRequests: ProductVariantRequestDto[] = [];
  productVariantTwos: ProductVariantDto[][] = [];
  productVariantTwoRequests: ProductVariantRequestDto[][] = [];
  checkAddProductClassify: boolean = false;
  receiveData(data: any) {
    this.productOptions = data;
  }

  receiveVariantData(data: any) {
    this.productVariants = data;
  }

  receiveVariantTwoData(data: any) {
    this.productVariantTwos = data;
  }

  receiveCheckAddData(data: boolean) {
    this.checkAddProductClassify = data;
  }

  receiveImageVariantData(data: any) {
    this.imageVariants = data;
  }

  receiveAtrributeNameData(data: any) {
    this.attributeNames = data;
  }

  createProduct() {
    this.productCreateRequest.isActive =
      this.defaultOptionActive === 'True' ? true : false;
    this.productCreateRequest.isVisibility =
      this.defaultOptionVisibily === 'True' ? true : false;

    const convertedProductOptions: { [key: string]: string[] } =
      this.productOptions.reduce((acc: { [key: string]: string[] }, obj) => {
        acc[obj.key] = obj.value;
        return acc;
      }, {});
    console.log('convertedProductOptions :>> ', convertedProductOptions);
    this.productCreateRequest.productOptions = convertedProductOptions;

    if (this.isAddVisibility) {
      let productVariant: ProductVariantDto = {
        attribute_names: [''],
        sku: this.skuProduct,
        quantity: this.quantityProduct,
        price: this.priceProduct,
        thumbnail_picture: '',
      };
      this.productCreateRequest.variantProducts.push(
        this.convertToProductVariantRequestDto(productVariant)
      );
    } else {
      if (!this.checkAddProductClassify) {
        for (let i = 0; i < this.productVariants.length; i++) {
          this.productVariants[i].attribute_names = this.attributeNames[i];
        }
        this.productVariants.pop();
        this.productCreateRequest.variantProducts =
          this.convertToProductVariantRequestArray(this.productVariants);
      } else {
        this.productVariantTwos.pop();
        this.productCreateRequest.variantProducts =
          this.convertToProductVariantRequestArray(
            this.productVariantTwos.flat()
          );
        for (
          let i = 0;
          i < this.productCreateRequest.variantProducts.length;
          i++
        ) {
          this.productCreateRequest.variantProducts[i].attributeNames =
            this.attributeNames[i];
        }
      }
    }
    console.log('this.productCreateRequest :>> ', this.productCreateRequest);
    this.callApis(this.productCreateRequest, this.images);
  }

  async callApis(productCreateDto: ProductCreateDto, imageFiles: File[]) {
    try {
      const response1 = await this.productService
        .createProduct(productCreateDto)
        .toPromise();
      const formData = new FormData();
      imageFiles.forEach((image) => formData.append('image', image));
      const response2 = await this.productService
        .createImageProduct(formData)
        .toPromise();
      this.router.navigateByUrl('/product');
    } catch (error) {
      // Xử lý lỗi
    }
  }

  // callAPICreateProduct(productCreateDto: ProductCreateDto): void {
  //   this.productService.createProduct(productCreateDto).subscribe(
  //     (response) => {
  //       console.log('Product created successfully');
  //       // Handle the response as needed
  //     },
  //     (error) => {
  //       console.error('Error creating product:', error);
  //       // Handle the error as needed
  //     }
  //   );
  // }

  convertToFile(fileContext: string, fileName: string) {
    if (fileContext === '') {
      console.log('Hello :>> ');
      return null;
    }
    // Xác định loại tệp tin
    const fileType = 'image/png';

    // Lấy phần dữ liệu cơ bản (base64 data) từ chuỗi base64
    const base64Data = fileContext.split(',')[1];

    // Chuyển đổi chuỗi base64 thành mảng byte
    const byteCharacters = atob(base64Data);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);

    // Tạo đối tượng IFormFile từ mảng byte
    return new File([byteArray], fileName + '.png', { type: fileType });
  }

  convertToProductVariantRequestArray(
    dtos: ProductVariantDto[]
  ): ProductVariantRequestDto[] {
    return dtos.map((dto: ProductVariantDto) => {
      const requestDto: ProductVariantRequestDto = {
        attributeNames: dto.attribute_names,
        sKU: dto.sku,
        quantity: dto.quantity,
        price: dto.price,
        // thumbnailPicture: this.convertToFile(
        //   dto.thumbnail_picture,
        //   dto.attribute_names.join('')
        // ),
        thumbnailPicture: dto.thumbnail_picture,
      };
      return requestDto;
    });
  }

  convertToProductVariantRequestDto(
    dto: ProductVariantDto
  ): ProductVariantRequestDto {
    const requestDto: ProductVariantRequestDto = {
      attributeNames: dto.attribute_names,
      sKU: dto.sku,
      quantity: dto.quantity,
      price: dto.price,
      // thumbnailPicture: this.convertToFile(
      //   dto.thumbnail_picture,
      //   dto.attribute_names.join('')
      // ),
      thumbnailPicture: dto.thumbnail_picture,
    };

    return requestDto;
  }

  // convertToProductVariantTwoRequestArray(dtos: ProductVariantDto[][]): ProductVariantRequestDto[][] {
  //   return dtos.map((innerArray: ProductVariantDto[]) => {
  //     return innerArray.map((dto: ProductVariantDto) => {
  //       const requestDto: ProductVariantRequestDto = {
  //         attribute_names: dto.attribute_names,
  //         sku: dto.sku,
  //         quantity: dto.quantity,
  //         price: dto.price,
  //         thumbnail_picture: new File([dto.thumbnail_picture], 'fileName.png')
  //       };
  //       return requestDto;
  //     });
  //   });
  // }
}
