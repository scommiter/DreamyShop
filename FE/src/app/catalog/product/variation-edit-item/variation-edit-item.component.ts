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
  options: { value: string }[][] = [[{ value: '' }]];
  containerVariantCount: number = 0;
  indexProductVariant: number = 1;
  optionTitles: string[] = [];
  optionTitle: string = '';
  indexOptionTitle: number = 0;

  constructor() {}

  ngOnInit(): void {}

  onInputFocus(index: number) {
    if (this.options[index][this.options[index].length - 1].value !== '') {
      this.options[index].push({ value: '' });
    }
    console.log('this.productOptions :>> ', this.options);
  }

  addProductVariant() {
    this.productOptions.push('');
    this.optionTitles.push('');
    this.options.push([{ value: '' }]);
    this.containerVariantCount++;
    this.indexProductVariant++;
    this.indexOptionTitle++;
  }

  onInputOptionTile() {
    console.log('this.optionTitles :>> ', this.optionTitles);
    console.log('this.indexOptionTitle :>> ', this.indexOptionTitle);
  }

  onDeleteClassify(index: number) {
    this.options[index].splice(this.options[index].length - 3, 1);
  }
}
