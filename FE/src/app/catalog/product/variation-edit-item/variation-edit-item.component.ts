import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductVariantDto } from 'src/app/shared/models/product-variant.dto';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-variation-edit-item',
  templateUrl: './variation-edit-item.component.html',
  styleUrls: ['./variation-edit-item.component.scss'],
})
export class VariationEditItemComponent {
  @Input() productOptionKey: string = '';
  @Input() productOptionValue: string[] = [];
  @Input() indexProductOption: number = 1;

  addClassifyProduct: boolean = false;
  @Output() productOptionOuputs = new EventEmitter<
    { key: string; value: string[] }[]
  >();
  @Output() productVariantOutputs = new EventEmitter<ProductVariantDto[]>();
  @Output() productVariantTwoOutputs = new EventEmitter<
    ProductVariantDto[][]
  >();
  @Output() checkAddProductClassify = new EventEmitter<boolean>();
  productOptions: { key: string; value: string[] }[] = [
    { key: '', value: [''] },
  ];
  @Output() imageVariantOutputs = new EventEmitter<File[]>();
  @Output() atrributeNameOutputs = new EventEmitter<string[][]>();
  @Output() checkSKU = new EventEmitter<boolean>();
  productVariants: ProductVariantDto[] = [
    {
      attribute_names: [''],
      sku: '',
      quantity: 0,
      price: 0,
      thumbnail_picture: '',
    },
  ];
  productVariantTwos: ProductVariantDto[][] = [
    [
      {
        attribute_names: [''],
        sku: '',
        quantity: 0,
        price: 0,
        thumbnail_picture: '',
      },
    ],
  ];
  isVisibilityTwo: boolean[][] = [];
  checkCountOptionsOne: number = 0;
  checkIsAddVariant: boolean = false;
  imageVariants: File[] = [];

  constructor(private messageService: MessageService) {}

  addClassifyProductVariant() {
    this.addClassifyProduct = true;
    this.productOptions.push({ key: '', value: [''] });
    this.checkIsAddVariant = true;
    this.checkAddProductClassify.emit(this.addClassifyProduct);
  }

  onInputOptionValue(index: number, value: string, indexChild: number) {
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
            attribute_names: [''],
            sku: '',
            quantity: 0,
            price: 0,
            thumbnail_picture: '',
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
          attribute_names: [''],
          sku: '',
          quantity: 0,
          price: 0,
          thumbnail_picture: '',
        });
        this.isVisibility.push(true);
      }
    }
  }

  onInputValue() {
    this.productOptionOuputs.emit(this.productOptions);
    this.productVariantOutputs.emit(this.productVariants);
    if (this.productOptions.length > 1) {
      this.productVariantTwoOutputs.emit(this.productVariantTwos);
    }
    let productOptions = this.convertProductOptionsToAttributeName(
      this.productOptions
    );
    this.atrributeNameOutputs.emit(productOptions);
  }

  skuCaches: string[] = [];
  checkCssSKU: boolean = false;
  onInputSKUValue(sku: string, index: number) {
    this.productOptionOuputs.emit(this.productOptions);
    this.productVariantOutputs.emit(this.productVariants);
    if (this.productOptions.length > 1) {
      this.productVariantTwoOutputs.emit(this.productVariantTwos);
    }
    let productOptions = this.convertProductOptionsToAttributeName(
      this.productOptions
    );
    this.atrributeNameOutputs.emit(productOptions);
    this.checkCssSKU = false;

    //check sku of product variant is unique
    if (this.productOptions.length == 1) {
      for (let i = 0; i < this.productVariants.length - 1; i++) {
        if (sku === this.productVariants[i].sku && index !== i) {
          this.showWarn();
          this.checkSKU.emit(true);
          this.checkCssSKU = true;
          break;
        }
      }
    } else if (this.productOptions.length == 2) {
      let skus = this.productVariantTwos
        .flat()
        .filter((item) => item.sku !== '')
        .map((item) => item.sku);
      if (this.skuCaches.includes(sku)) {
        this.showWarn();
        this.checkSKU.emit(true);
        this.checkCssSKU = true;
      }
      this.skuCaches = skus;
    }
    if (!this.checkCssSKU) {
      this.checkSKU.emit(false);
    }
  }

  showWarn() {
    this.messageService.add({
      severity: 'warn',
      summary: 'Warn',
      detail: 'SKU phải là duy nhất',
    });
  }

  onDeleteVariant(index: number, indexChild: number) {
    this.productOptions[index].value.splice(indexChild, 1);
    if (index === 0) {
      this.checkCountOptionsOne = this.productOptions[0].value.length;
    }
  }

  //trackby return index of of the element inside the loop to determine the change
  trackByFn(index: number, item: { key: string; value: string[] }) {
    return index;
  }
  trackByFnNested(index: number, item: any) {
    return index;
  }

  //upload thumnail Image
  isVisibility: boolean[] = [];
  url: string[] = [''];
  onSelectFile(event: any, indexOption: number) {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0] as File;
      this.imageVariants.push(file);
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => {
        // called once readAsDataURL is completed
        this.productVariants[indexOption].thumbnail_picture = event.target
          ?.result as string;
        this.url.push(event.target?.result as string);
      };
      this.isVisibility[indexOption] = false;
    }
  }

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
        this.productVariantTwos[indexOption1][indexOption2].thumbnail_picture =
          event.target?.result as string;
        this.url.push(event.target?.result as string);
      };
      this.isVisibilityTwo[indexOption1][indexOption2] = false;
    }
  }

  removeNullObject(productOptions: { key: string; value: string[] }[]) {
    for (let i = 0; i < productOptions.length; i++) {
      productOptions[i].value.pop();
    }
    return productOptions;
  }

  convertProductOptionsToAttributeName(
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
          index++;
        }
      }
    } else {
      for (let i = 0; i < productOptions[0].value.length - 1; i++) {
        attributeNames.push([]);
        attributeNames[i].push(productOptions[0].value[i]);
      }
    }

    return attributeNames;
  }
}
