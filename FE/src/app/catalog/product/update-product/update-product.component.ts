import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewEncapsulation,
} from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem, MessageService } from 'primeng/api';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { ProductCreateDto } from 'src/app/shared/models/product-create-update.dto';
import {
  ProductVariantDto,
  ProductVariantRequestDto,
} from 'src/app/shared/models/product-variant.dto';
import { ProductDto } from 'src/app/shared/models/product.dto';
import { MatDialogRef } from '@angular/material/dialog';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class UpdateProductComponent implements OnInit {
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
  productUpdateRequest: ProductCreateDto = {
    name: '',
    code: '',
    productType: ProductTypes.Single,
    categoryName: '',
    manufacturerName: '',
    description: '',
    isActive: true,
    isVisibility: true,
    images: [],
    productOptions: {},
    variantProducts: [],
  };
  items: MenuItem[] = [];
  home: MenuItem = {};
  productTypes: string[] = [];
  stateOptions: string[] = [];
  isActiveProduct: string = 'False';
  isVisibilityProduct: string = 'False';
  imageCount: number = 0;
  images: File[] = [];
  imageProducts: string[] = [];
  url: string[] = [''];
  isAddVisibility: boolean = true;
  skuProduct: string = '';
  priceProduct: number = 0;
  quantityProduct: number = 0;
  checkCountProductOptions: number = 0;
  isSKUduplicate: boolean = false;

  constructor(
    public productService: ProductService,
    private ref: DynamicDialogRef,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.productUpdate = this.productService.getProductUpdate();
    this.productTypes = [
      'Single',
      'Grouped',
      'Configurable',
      'Bundle',
      'Virtual',
      'Downloadable',
    ];
    this.stateOptions = ['True', 'False'];
    this.productUpdate.productType = Object.values(ProductTypes)[
      this.productUpdate.productType - 1
    ] as ProductTypes;
    this.productUpdate.isActive == true
      ? (this.isActiveProduct = 'True')
      : 'False';
    this.productUpdate.isVisibility == true
      ? (this.isVisibilityProduct = 'True')
      : 'False';
    this.imageCount = this.productUpdate.thumbnailPictures.length;
    this.imageProducts = this.productUpdate.thumbnailPictures;
    if (this.productUpdate.productAttributeDisplayDtos.length > 1) {
      this.isAddVisibility = false;
    }
    this.skuProduct = this.productUpdate.productAttributeDisplayDtos[0].sku;
    this.priceProduct = this.productUpdate.productAttributeDisplayDtos[0].price;
    this.quantityProduct =
      this.productUpdate.productAttributeDisplayDtos[0].quantity;
    this.checkCountProductOptions = this.productUpdate.optionNames.length;
  }

  closeImage(index: number) {
    this.imageProducts.splice(index, 1);
    this.images.splice(index, 1);
    this.imageCount--;
  }

  onSelectFile(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      let cacheNumberImage = this.imageCount;
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

  //receive Data
  productVariantOutputs: ProductVariantRequestDto[][] = [];
  productOptionOutputs: { key: string; value: string[] }[] = [];

  receiveProductOptionCaseOneData(data: any) {
    if (this.checkCountProductOptions === 1) {
      this.productOptionOutputs = data;
    }
    this.checkIsUpdateOptionVariant = true;
  }

  receiveVariantCaseOneData(data: any) {
    if (this.checkCountProductOptions === 1) {
      this.productVariantOutputs = data;
    }
    this.checkIsUpdateOptionVariant = true;
  }

  receiveProductOptionCaseTwoData(data: any) {
    if (this.checkCountProductOptions === 2) {
      this.productOptionOutputs = data;
    }
    this.checkIsUpdateOptionVariant = true;
  }

  receiveVariantCaseTwoData(data: any) {
    if (this.checkCountProductOptions === 2) {
      this.productVariantOutputs = data;
    }
    this.checkIsUpdateOptionVariant = true;
  }

  receiveCheckSKU(data: any) {
    this.isSKUduplicate = data;
    this.checkIsUpdateOptionVariant = true;
  }

  addProductOptions() {
    this.isAddVisibility = false;
    this.checkIsUpdateOptionVariant = true;
  }

  checkIsUpdateOptionVariant: boolean = false;
  updateProduct() {
    if (this.checkCountProductOptions === 1) {
      this.productVariantOutputs.pop();
    }
    this.productUpdateRequest.name = this.productUpdate.name;
    this.productUpdateRequest.code = this.productUpdate.code;
    this.productUpdateRequest.productType = this.productUpdate.productType;
    this.productUpdateRequest.categoryName = this.productUpdate.categoryName;
    this.productUpdateRequest.manufacturerName =
      this.productUpdate.manufacturerName;
    this.productUpdateRequest.description = this.productUpdate.description;
    this.productUpdateRequest.isActive = this.productUpdate.isActive;
    this.productUpdateRequest.isVisibility = this.productUpdate.isVisibility;
    this.productUpdateRequest.images = this.productUpdate.thumbnailPictures;

    const convertedProductOptions: { [key: string]: string[] } =
      this.productOptionOutputs.reduce(
        (acc: { [key: string]: string[] }, obj) => {
          acc[obj.key] = obj.value;
          return acc;
        },
        {}
      );
    if (this.checkIsUpdateOptionVariant) {
      this.productUpdateRequest.productOptions = convertedProductOptions;

      if (this.checkCountProductOptions !== 0) {
        this.productUpdateRequest.variantProducts =
          this.convertToVariantProducts(
            this.productVariantOutputs.flat(),
            this.productOptionOutputs
          );
      } else {
        if (this.isAddVisibility) {
          let productVariant: ProductVariantDto = {
            attribute_names: [''],
            sku: this.skuProduct,
            quantity: this.quantityProduct,
            price: this.priceProduct,
            thumbnail_picture: '',
          };
          this.productUpdateRequest.variantProducts.push(
            this.convertToProductVariantRequestDto(productVariant)
          );
        } else {
          if (!this.checkAddProductClassify) {
            for (let i = 0; i < this.productVariants.length; i++) {
              this.productVariants[i].attribute_names = this.attributeNames[i];
            }
            this.productVariants.pop();
            this.productUpdateRequest.variantProducts =
              this.convertToProductVariantRequestArray(this.productVariants);
          } else {
            this.productVariantTwos.pop();
            this.productUpdateRequest.variantProducts =
              this.convertToProductVariantRequestArray(
                this.productVariantTwos.flat()
              );
            for (
              let i = 0;
              i < this.productUpdateRequest.variantProducts.length;
              i++
            ) {
              this.productUpdateRequest.variantProducts[i].attributeNames =
                this.attributeNames[i];
            }
          }
        }
      }
    }
    console.log(this.checkIsUpdateOptionVariant);
    this.productService
      .updateProduct(this.productUpdate.id, this.productUpdateRequest)
      .subscribe({
        next: () => {
          this.ref.close(this);
          this.showSuccess();
        },
        error: (err) => {
          this.showEror();
        },
      });
    this.updateCompleted.emit();
  }

  showSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Thành công',
      detail: 'Cập nhật sản phẩm thành công',
    });
  }

  showEror() {
    this.messageService.add({
      severity: 'erorr',
      summary: 'Thất bại',
      detail: 'Cập nhật sản phẩm thất bại. Hãy kiểm tra quyền của tài khoản.',
    });
  }

  @Output() updateCompleted: EventEmitter<void> = new EventEmitter<void>();

  convertToVariantProducts(
    productVariantOutputs: ProductVariantRequestDto[],
    productOptions: { key: string; value: string[] }[]
  ) {
    const attributeNames: string[][] = [];
    if (productOptions.length > 1) {
      let index = 0;
      for (let i = 0; i < productOptions[0].value.length - 1; i++) {
        for (let j = 0; j < productOptions[1].value.length - 1; j++) {
          attributeNames.push([]);
          attributeNames[index].push(productOptions[0].value[i]);
          attributeNames[index].push(productOptions[1].value[j]);
          productVariantOutputs[index].attributeNames = attributeNames[index];
          index++;
        }
      }
    } else {
      for (let i = 0; i < productOptions[0].value.length - 1; i++) {
        attributeNames.push([]);
        attributeNames[i].push(productOptions[0].value[i]);
        productVariantOutputs[i].attributeNames = attributeNames[i];
      }
    }
    return productVariantOutputs;
  }

  //Case Add product variant
  productOptions: { key: string; value: string[] }[] = [];
  productVariants: ProductVariantDto[] = [];
  productVariantRequests: ProductVariantRequestDto[] = [];
  productVariantTwos: ProductVariantDto[][] = [];
  productVariantTwoRequests: ProductVariantRequestDto[][] = [];
  checkAddProductClassify: boolean = false;
  imageVariants: File[] = [];
  attributeNames: string[][] = [];
  receiveData(data: any) {
    if (this.checkCountProductOptions === 0) {
      this.productOptionOutputs = data;
    }
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
}
