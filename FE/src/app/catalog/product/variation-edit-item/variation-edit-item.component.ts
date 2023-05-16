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

  addClassifyProductVariant() {
    this.addClassifyProduct = true;
    this.productOptions.push({ key: '', value: [''] });
  }

  onInputOptionValue(index: number, value: string, indexChild: number) {
    console.log('this.productOptions :>> ', this.productOptions);
    console.log('indexChild :>> ', indexChild);
    if (
      value !== '' &&
      indexChild === this.productOptions[index].value.length - 1
    ) {
      this.productOptions[index].value.push('');
      console.log('this.productOptions :>> ', this.productOptions);
    }
  }

  onDeleteVariant(index: number, indexChild: number) {
    this.productOptions[index].value.splice(indexChild, 1);
  }

  //trackby return index of of the element inside the loop to determine the change
  trackByFn(index: number, item: { key: string; value: string[] }) {
    return index;
  }
  trackByFnNested(index: number, item: any) {
    return index;
  }
}
