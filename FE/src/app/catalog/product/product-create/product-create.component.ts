import { Component, OnDestroy } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuItem, MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import { ProductCreateDto } from 'src/app/shared/models/product-create-update.dto';
import {
  ProductVariantDto,
  ProductVariantRequestDto,
} from 'src/app/shared/models/product-variant.dto';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss'],
})
export class ProductCreateComponent implements OnDestroy {
  public formProductCreate: FormGroup;
  private ngUnsubsribe = new Subject<void>();
  items: MenuItem[] = [];
  home: MenuItem = {};
  productTypes: string[] = [];
  stateOptions: string[] = [];
  productCreateRequest: ProductCreateDto = {
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

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private messageService: MessageService,
    private route: ActivatedRoute
  ) {
    this.formProductCreate = this.fb.group({
      name: new FormControl('', [
        Validators.minLength(3),
        Validators.maxLength(20),
        Validators.required,
      ]),
      code: new FormControl('', [
        Validators.minLength(3),
        Validators.maxLength(10),
        Validators.required,
      ]),
      description: new FormControl('', [Validators.maxLength(200)]),
      productType: new FormControl('', [Validators.required]),
      categoryName: new FormControl('', [Validators.required]),
      manufacturerName: new FormControl('', [Validators.required]),
      isActive: new FormControl('True'),
      isVisibily: new FormControl('True'),
      sku: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required]),
      quantity: new FormControl('', [Validators.required]),
    });
  }

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

  ngOnDestroy(): void {
    this.ngUnsubsribe.next();
    this.ngUnsubsribe.complete();
  }

  saveChange() {
    //Convert modal
    this.productCreateRequest.isActive =
      this.formProductCreate.get('isActive')?.value === 'True' ? true : false;
    this.productCreateRequest.isVisibility =
      this.formProductCreate.get('isVisibily')?.value === 'True' ? true : false;
    this.productCreateRequest.categoryName =
      this.formProductCreate.get('categoryName')?.value;
    this.productCreateRequest.productType =
      this.formProductCreate.get('productType')?.value;
    this.productCreateRequest.code = this.formProductCreate.get('code')?.value;
    this.productCreateRequest.description =
      this.formProductCreate.get('description')?.value;
    this.productCreateRequest.manufacturerName =
      this.formProductCreate.get('manufacturerName')?.value;
    this.productCreateRequest.name = this.formProductCreate.get('name')?.value;

    const convertedProductOptions: { [key: string]: string[] } =
      this.productOptions.reduce((acc: { [key: string]: string[] }, obj) => {
        acc[obj.key] = obj.value;
        return acc;
      }, {});
    this.productCreateRequest.productOptions = convertedProductOptions;

    if (this.isAddVisibility) {
      let productVariant: ProductVariantDto = {
        attribute_names: [''],
        sku: this.formProductCreate.get('sku')?.value,
        quantity: this.formProductCreate.get('quantity')?.value,
        price: this.formProductCreate.get('price')?.value,
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
    this.productCreateRequest.images = this.imageProducts;
    console.log('this.productCreateRequest :>> ', this.productCreateRequest);
    //call api
    this.productService
      .createProduct(this.productCreateRequest)
      .pipe(takeUntil(this.ngUnsubsribe))
      .subscribe({
        next: () => {
          this.productService.SetCreateSuccess(true);
          this.router.navigate(['/product']);
        },
        error: () => {},
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

  //check input
  isFieldEmpty(fieldName: string): boolean {
    const field = this.formProductCreate.get(fieldName);
    return field !== null && field.invalid && (field.dirty || field.touched);
  }

  //import image product
  imageProducts: string[] = [];
  images: File[] = [];
  imageCount: number = 0;
  url: string[] = [''];
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

  closeImage(index: number) {
    this.imageProducts.splice(index, 1);
    this.images.splice(index, 1);
    this.imageCount--;
  }

  //PRODUCT VARIANT
  isAddVisibility: boolean = true;
  isSKUduplicate: boolean = false;
  addProductOptions() {
    this.isAddVisibility = false;
    //clear validator
    const formData = {
      sku: 'clear',
      price: 0,
      quantity: 0,
    };
    this.formProductCreate.patchValue(formData);
  }
  productOptions: { key: string; value: string[] }[] = [];
  productVariants: ProductVariantDto[] = [];
  productVariantRequests: ProductVariantRequestDto[] = [];
  productVariantTwos: ProductVariantDto[][] = [];
  productVariantTwoRequests: ProductVariantRequestDto[][] = [];
  checkAddProductClassify: boolean = false;
  imageVariants: File[] = [];
  attributeNames: string[][] = [];
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

  receiveCheckSKU(data: any) {
    this.isSKUduplicate = data;
  }
}
