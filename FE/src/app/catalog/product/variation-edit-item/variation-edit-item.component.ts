import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-variation-edit-item',
  templateUrl: './variation-edit-item.component.html',
  styleUrls: ['./variation-edit-item.component.scss'],
})
export class VariationEditItemComponent implements OnInit {
  @Input() productOptionKey: string = '';
  @Input() productOptionValue: string[] = [];
  @Input() indexProductOption: number = 1;

  productOptions: string[] = [''];
  indexProductVariant: number = 1;
  options: { value: string }[][] = [[{ value: '' }]];
  containerVariantCount: number = 0;

  constructor() {}

  ngOnInit(): void {}

  onInputFocus() {
    let index = this.indexProductVariant - 1;
    if (
      this.options[index].length === 0 ||
      this.options[index][this.options[index].length - 1].value !== ''
    ) {
      this.options[index].push({ value: '' });
    }
    console.log('this.productOptions :>> ', this.options);
  }

  addProductVariant() {
    this.productOptions.push('');
    this.options.push([{ value: '' }]);
    this.indexProductVariant++;
    this.containerVariantCount++;
  }

  onDeleteClassify() {
    this.options[this.indexProductVariant - 1].splice(
      this.options[this.indexProductVariant - 1].length - 2,
      1
    );
  }
}
