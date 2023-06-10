import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { ProductTypes } from 'src/app/shared/enums/product-types.enum';
import {
  ProductVariantDto,
  ProductVariantRequestDto,
} from 'src/app/shared/models/product-variant.dto';
import {
  ProductAttributeDisplayDto,
  ProductDto,
} from 'src/app/shared/models/product.dto';

@Component({
  selector: 'app-variation-update-item',
  templateUrl: './variation-update-item.component.html',
  styleUrls: ['./variation-update-item.component.scss'],
})
export class VariationUpdateItemComponent implements OnInit {
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
  productOptions: { key: string; value: string[] }[] = [];
  addClassifyProduct: boolean = false;
  productVariantTwos: ProductVariantRequestDto[][] = [];
  productVariants: ProductVariantRequestDto[] = [];
  isVisibilityTwo: boolean[][] = [];
  checkCountOptionsOne: number = 0;
  checkIsAddVariant: boolean = false;
  imageVariants: File[] = [];
  checkNumberOfOptionNames: number = 0;
  @Output() productVariantOutputsCaseOne = new EventEmitter<
    ProductVariantRequestDto[][]
  >();
  @Output() productOptionOutputsCaseOne = new EventEmitter<
    { key: string; value: string[] }[]
  >();
  @Output() productVariantOutputsCaseTwo = new EventEmitter<
    ProductVariantRequestDto[][]
  >();
  @Output() productOptionOutputsCaseTwo = new EventEmitter<
    { key: string; value: string[] }[]
  >();

  constructor(public productService: ProductService) {}

  ngOnInit(): void {
    this.productUpdate = this.productService.getProductUpdate();
    console.log('this.productUpdate :>> ', this.productUpdate);
    this.checkNumberOfOptionNames = this.productUpdate.optionNames.length;
    if (this.checkNumberOfOptionNames !== 0) {
      this.convertToProductOptions(
        this.productUpdate.optionNames,
        this.productUpdate.productAttributeDisplayDtos
      );
      this.productUpdate.optionNames.length > 1
        ? (this.addClassifyProduct = true)
        : (this.addClassifyProduct = false);
      if (this.addClassifyProduct == true) {
        this.convertToProductVariantTwos(
          this.productUpdate.productAttributeDisplayDtos
        );
      } else {
        this.convertToProductVariant(
          this.productUpdate.productAttributeDisplayDtos
        );
      }
    } else {
      this.productOptions = [{ key: '', value: [''] }];
    }
  }

  convertToProductOptions(
    optionNames: string[],
    productAttributeDisplayDtos: ProductAttributeDisplayDto[]
  ) {
    for (let i = 0; i < optionNames.length; i++) {
      this.productOptions.push({ key: optionNames[i], value: [] });
      for (let j = 0; j < productAttributeDisplayDtos.length; j++) {
        this.productOptions[i].value.push(
          productAttributeDisplayDtos[j].attributeNames[i]
        );
      }
      this.productOptions[i].value = [...new Set(this.productOptions[i].value)];
      this.productOptions[i].value.push('');
    }
  }

  convertToProductVariant(productAttributes: ProductAttributeDisplayDto[]) {
    for (let i = 0; i < productAttributes.length; i++) {
      this.productVariants.push({
        attributeNames: productAttributes[i].attributeNames,
        sKU: productAttributes[i].sku,
        quantity: productAttributes[i].quantity,
        price: productAttributes[i].price,
        thumbnailPicture: productAttributes[i].image,
      });
    }
    console.log('productAttributes :>> ', productAttributes);
  }

  convertToProductVariantTwos(productAttributes: ProductAttributeDisplayDto[]) {
    let index = 0;
    for (let i = 0; i < this.productOptions[0].value.length - 1; i++) {
      this.productVariantTwos.push([]);
      for (let j = 0; j < this.productOptions[1].value.length - 1; j++) {
        this.productVariantTwos[i].push({
          attributeNames: [''],
          sKU: '',
          quantity: 0,
          price: 0,
          thumbnailPicture: '',
        });
        let productOptionValues: string[] = [];
        productOptionValues.push(this.productOptions[0].value[i]);
        productOptionValues.push(this.productOptions[1].value[j]);
        this.productVariantTwos[i][j].attributeNames = productOptionValues;
        this.productVariantTwos[i][j].sKU = productAttributes[index].sku;
        this.productVariantTwos[i][j].quantity =
          productAttributes[index].quantity;
        this.productVariantTwos[i][j].price = productAttributes[index].price;
        this.productVariantTwos[i][j].thumbnailPicture =
          productAttributes[index].image;
        index++;
      }
    }
    console.log('this.productVariantTwos :>> ', this.productVariantTwos);
  }

  addClassifyProductVariant() {
    this.addClassifyProduct = true;
    this.productOptions.push({ key: '', value: [''] });
    this.checkIsAddVariant = true;
  }

  onInputValue() {
    this.productVariantOutputsCaseOne.emit(this.productVariantTwos);
    this.productOptionOutputsCaseOne.emit(this.productOptions);
    this.productVariantOutputsCaseTwo.emit(this.productVariantTwos);
    this.productOptionOutputsCaseTwo.emit(this.productOptions);
    console.log('HELLLLLLLLLLLLLLLLLLLLLLLL :>> ', this.productVariantTwos);
  }

  onInputOptionValue(index: number, value: string, indexChild: number) {
    if (this.checkNumberOfOptionNames !== 1) {
      this.handleInputOptionHasTwo(index, value, indexChild);
    } else {
      this.handleInputOptionHasOne(index, value, indexChild);
    }
    this.productVariantOutputsCaseOne.emit(this.productVariantTwos);
    this.productOptionOutputsCaseOne.emit(this.productOptions);
    this.productVariantOutputsCaseTwo.emit(this.productVariantTwos);
    this.productOptionOutputsCaseTwo.emit(this.productOptions);
  }

  handleInputOptionHasTwo(index: number, value: string, indexChild: number) {
    if (
      value !== '' &&
      indexChild === this.productOptions[index].value.length - 1 &&
      index === 0
    ) {
      this.productOptions[index].value.push('');
      this.productVariantTwos.push([]);
      let indexVariant = this.productVariantTwos.length - 1;
      for (let i = 0; i < this.productOptions[1].value.length - 1; i++) {
        this.productVariantTwos[indexVariant].push({
          attributeNames: [''],
          sKU: '',
          quantity: 0,
          price: 0,
          thumbnailPicture: '',
        });
      }

      console.log('Hellooooooooo', this.productOptions[index]);
      console.log('this.productVariantTwos', this.productVariantTwos);
    } else if (
      value !== '' &&
      indexChild === this.productOptions[index].value.length - 1 &&
      index === 1
    ) {
      this.productOptions[index].value.push('');
      for (let i = 0; i < this.productVariantTwos.length; i++) {
        this.productVariantTwos[i].push({
          attributeNames: [''],
          sKU: '',
          quantity: 0,
          price: 0,
          thumbnailPicture: '',
        });
      }
    }
  }

  handleInputOptionHasOne(index: number, value: string, indexChild: number) {
    if (
      (index === 1 && this.checkIsAddVariant == true) ||
      (this.productOptions[0].value.length !== this.checkCountOptionsOne &&
        this.checkIsAddVariant == true)
    ) {
      this.productVariantTwos = [];
      this.isVisibilityTwo = [];
      for (let i = 0; i < this.productOptions[0].value.length; i++) {
        this.productVariantTwos[i] = [];
        this.isVisibilityTwo[i] = [];
        for (let j = 0; j < this.productOptions[1].value.length; j++) {
          this.productVariantTwos[i].push({
            attributeNames: [''],
            sKU: '',
            quantity: 0,
            price: 0,
            thumbnailPicture: '',
          });
          this.isVisibilityTwo[i].push(true);
        }
      }
    }
    if (
      value !== '' &&
      indexChild === this.productOptions[index].value.length - 1
    ) {
      this.productOptions[index].value.push('');
      if (index === 0 && this.checkIsAddVariant == false) {
        this.checkCountOptionsOne = this.productOptions[0].value.length;
        this.productVariants.push({
          attributeNames: [''],
          sKU: '',
          quantity: 0,
          price: 0,
          thumbnailPicture: '',
        });
        this.isVisibility.push(true);
      }
    }
  }

  onDeleteVariant(index: number, indexChild: number) {
    this.productOptions[index].value.splice(indexChild, 1);
    if (index === 0) {
      this.productVariantTwos.splice(index, 1);
      this.checkCountOptionsOne = this.productOptions[0].value.length;
    }
    if (index === 1) {
      for (let i = 0; i < this.productOptions[0].value.length - 1; i++) {
        this.productVariantTwos[i].splice(indexChild, 1);
      }
    }
  }

  //upload thumnail Image
  isVisibility: boolean[] = [];
  url: string[] = [''];
  onSelectFile(event: any, indexOption: number) {}
  onSelectFileVariantTwo(
    event: any,
    indexOption1: number,
    indexOption2: number
  ) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => {
        // called once readAsDataURL is completed
        this.productVariantTwos[indexOption1][indexOption2].thumbnailPicture =
          event.target?.result as string;
        this.url.push(event.target?.result as string);
      };
      this.isVisibilityTwo[indexOption1][indexOption2] = false;
      this.productVariantOutputsCaseOne.emit(this.productVariantTwos);
      this.productOptionOutputsCaseOne.emit(this.productOptions);
      this.productVariantOutputsCaseTwo.emit(this.productVariantTwos);
      this.productOptionOutputsCaseTwo.emit(this.productOptions);
    }
  }

  //trackby return index of of the element inside the loop to determine the change
  trackByFn(index: number, item: { key: string; value: string[] }) {
    return index;
  }
  trackByFnNested(index: number, item: any) {
    return index;
  }
}
