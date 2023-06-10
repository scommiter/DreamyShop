import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
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
export class ProductCreateComponent {
  public formProductCreate: FormGroup;
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

  constructor(private fb: FormBuilder) {
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

  saveChange(loginFormValue: FormGroup) {
    console.log('this.form :>> ', loginFormValue);
    console.log('this.imageProducts :>> ', this.imageProducts);
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
}
