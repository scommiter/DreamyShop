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
      thumbnail_picture: [''],
    },
  ];
  productVariantTwos: ProductVariantDto[][] = [
    [
      {
        attribute_names: [''],
        sku: '',
        quantity: '',
        price: 0,
        thumbnail_picture: [''],
      },
    ],
  ];
  isVisibilityTwo: boolean[][] = [];
  checkCountOptionsOne: number = 0;

  addClassifyProductVariant() {
    this.addClassifyProduct = true;
    this.productOptions.push({ key: '', value: [''] });
    this.productVariants[0].thumbnail_picture = [''];
    this.isVisibility = [];
    console.log('this.productVariants :>> ', this.productVariants);
    console.log('this.isVisibility :>> ', this.isVisibility);
  }

  onInputOptionValue(index: number, value: string, indexChild: number) {
    if (
      index === 1 ||
      this.productOptions[0].value.length !== this.checkCountOptionsOne
    ) {
      //gan lai productVariantTwos moi khi nhap them option moi
      this.productVariantTwos = [
        [
          {
            attribute_names: [''],
            sku: '',
            quantity: '',
            price: 0,
            thumbnail_picture: [''],
          },
        ],
      ];
      for (let i = 0; i < this.productOptions[0].value.length - 1; i++) {
        for (let j = 0; j < this.productOptions[1].value.length - 1; j++) {
          this.productVariantTwos[i][j].thumbnail_picture.push('');
        }
      }
      console.log(
        'productVariantTwossssssssssss :>> ',
        this.productVariantTwos
      );
    }
    if (
      value !== '' &&
      indexChild === this.productOptions[index].value.length - 1
    ) {
      this.productOptions[index].value.push('');
      if (index === 0) {
        this.checkCountOptionsOne = this.productOptions[0].value.length;
        this.productVariants[index].thumbnail_picture.push('');
        this.isVisibility.push(true);
      }
      console.log('this.productOptions :>> ', this.productOptions);
      console.log('this.checkCountOptionsOne :>> ', this.checkCountOptionsOne);
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
  onSelectFile(event: any, index: number, indexOption: number) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => {
        // called once readAsDataURL is completed
        this.productVariants[index].thumbnail_picture[indexOption + 1] = event
          .target?.result as string;
        this.url.push(event.target?.result as string);
      };
      this.isVisibility[indexOption] = false;
    }
  }
}
