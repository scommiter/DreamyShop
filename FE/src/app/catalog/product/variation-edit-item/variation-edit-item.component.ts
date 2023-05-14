import { Component, Input, OnInit } from '@angular/core';
import { ProductVariantDto } from 'src/app/shared/models/product-variant.dto';

@Component({
  selector: 'app-variation-edit-item',
  templateUrl: './variation-edit-item.component.html',
  styleUrls: ['./variation-edit-item.component.scss'],
})
export class VariationEditItemComponent implements OnInit {
  @Input() productOptionKey: string = '';
  @Input() productOptionValue: string[] = [];
  @Input() indexProductOption: number = 1;

  productVariants = new Array<ProductVariantDto>();

  productOptions: string[] = [''];
  options: { value: string }[][] = [[{ value: '' }]];
  containerVariantCount: number = 0;
  indexProductVariant: number = 1;
  optionTitles: string[] = [];
  indexOptionTitle: number = 0;
  optionTitleSeconds: string[] = [];
  indexSecond: number = 0;

  cache2: number = 1;
  cahe3: number = 1;

  constructor() {}

  ngOnInit(): void {}

  onInputFocus(index: number, value: string) {
    if (
      this.options[index][this.options[index].length - 1].value !== '' &&
      value !== ''
    ) {
      this.options[index].push({ value: '' });
    }
  }

  addProductVariant() {
    this.productOptions.push('');
    this.optionTitles.push('');
    this.options.push([{ value: '' }]);
    this.containerVariantCount++;
    this.indexProductVariant++;
    this.indexOptionTitle++;
    this.options[0] = this.options[0].filter((item) => item.value !== '');
    // console.log('this.indexProductVariant :>> ', this.indexProductVariant);
  }

  // checkInput(value: string) {
  //   console.log('value :>> ', value);
  //   if (value) {

  //   }
  // }

  onInputOptionValue(value: string) {
    console.log('value :>> ', value);
    if (value !== '') {
      if (this.indexProductVariant - 1 === 0) {
        this.optionTitleSeconds.push(this.options[0][this.indexSecond++].value);
        // console.log('this.optionTitleSeconds :>> ', this.optionTitleSeconds);
      }
      // console.log('Optionsssssssssssss :>> ', this.options[0].length);
      if (this.indexProductVariant - 1 === 1) {
        console.log(
          'this.indexProductVariantssssssss :>> ',
          this.indexProductVariant
        );
        let test = 0;
        for (let i = 0; i < this.options[0].length - 1; i++) {
          if (i == 0) {
            this.optionTitleSeconds.splice(this.cahe3, 0, '');
            // console.log('indexif :>> ', this.cahe3);
          } else {
            this.optionTitleSeconds.splice(test + this.cache2, 0, '');
            // console.log('test :>> ', test + this.cache2);
            // console.log('indexelse :>> ', test + this.cache2);
          }

          // console.log('iiiiiiiiiiiiiiiiiiiiiii :>> ', this.cache3 + this.cache2);
          // console.log('this.optionTitleSeconds :>> ', this.optionTitleSeconds);
          test += 2;
        }
        this.cache2 += 2;
        this.cahe3++;
      }

      console.log('this.options :>> ', this.options);
      console.log('this.optionTitles :>> ', this.optionTitles);
    }
  }

  onInputOptionTile() {
    // console.log('this.optionTitles :>> ', this.optionTitles);
    // console.log('this.indexOptionTitle :>> ', this.indexOptionTitle);
    // console.log('this.optionTitleSeconds :>> ', this.optionTitleSeconds);
  }

  onDeleteClassify(index: number) {
    this.options[index].splice(this.options[index].length - 3, 1);
  }
}
