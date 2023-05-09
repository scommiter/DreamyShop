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

  containerVariants: string[] = [''];
  indexProductVariant: number = 1;
  inputs: { value: string }[][] = [[{ value: '' }]];
  containerVariantCount: number = 0;

  constructor() {}

  ngOnInit(): void {}

  onInputFocus() {
    let index = this.indexProductVariant - 1;
    if (
      this.inputs[index].length === 0 ||
      this.inputs[index][this.inputs[index].length - 1].value !== ''
    ) {
      this.inputs[index].push({ value: '' });
      console.log('this.inputs22222222222 :>> ', this.inputs[index]);
    }
  }

  addProductVariant() {
    this.containerVariants.push('');
    this.inputs.push([{ value: '' }]);
    this.indexProductVariant++;
    this.containerVariantCount++;
  }

  onDeleteClassify() {
    this.inputs[this.indexProductVariant - 1].splice(-1);
  }
}
