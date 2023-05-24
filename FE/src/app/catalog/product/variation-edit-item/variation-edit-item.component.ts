import { Component, Input, OnInit } from '@angular/core';
import { ProductVariantDto } from 'src/app/shared/models/product-variant.dto';

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
  productOptions: { key: string; value: string[] }[] = [
    { key: '', value: [''] },
  ];
  productVariants: ProductVariantDto[] = [
    {
      attribute_names: [''],
      sku: '',
      quantity: '',
      price: 0,
      thumbnail_picture: '',
    },
  ];
  productVariantTwos: ProductVariantDto[][] = [
    [
      {
        attribute_names: [''],
        sku: '',
        quantity: '',
        price: 0,
        thumbnail_picture: '',
      },
    ],
  ];
  isVisibilityTwo: boolean[][] = [];
  checkCountOptionsOne: number = 0;
  checkIsAddVariant: boolean = false;

  addClassifyProductVariant() {
    this.addClassifyProduct = true;
    this.productOptions.push({ key: '', value: [''] });
    this.checkIsAddVariant = true;
  }

  onInputOptionValue(index: number, value: string, indexChild: number) {
    console.log(
      'this.productOptions[0].value.length :>> ',
      this.productOptions[0].value.length
    );
    console.log('this.checkCountOptionsOne :>> ', this.checkCountOptionsOne);
    if (
      (index === 1 && this.checkIsAddVariant == true) ||
      (this.productOptions[0].value.length !== this.checkCountOptionsOne &&
        this.checkIsAddVariant == true)
    ) {
      console.log('heloooooooooooooo :>> ', index, indexChild);
      this.productVariantTwos = [];
      this.isVisibilityTwo = [];
      for (let i = 0; i < this.productOptions[0].value.length; i++) {
        this.productVariantTwos[i] = [];
        this.isVisibilityTwo[i] = [];
        for (let j = 0; j < this.productOptions[1].value.length; j++) {
          this.productVariantTwos[i].push({
            attribute_names: [''],
            sku: '',
            quantity: '',
            price: 0,
            thumbnail_picture: '',
          });
          this.isVisibilityTwo[i].push(true);
        }
      }
      console.log('this.productVariantTwos :>> ', this.productVariantTwos);
      console.log('this.isVisibilityTwo :>> ', this.isVisibilityTwo);
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
          quantity: '',
          price: 0,
          thumbnail_picture: '',
        });
        this.isVisibility.push(true);
      }
    }
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
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => {
        // called once readAsDataURL is completed
        this.productVariants[indexOption].thumbnail_picture = event.target
          ?.result as string;
        this.url.push(event.target?.result as string);
      };
      console.log('this.productVariants :>> ', this.productVariants);
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
      console.log('this.productVariants :>> ', this.productVariants);
      this.isVisibilityTwo[indexOption1][indexOption2] = false;
    }
  }
}
