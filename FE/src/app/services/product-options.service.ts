import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductOptionsService {
  productOptions = new Map<string, Array<string>>();
  constructor() {}

  getOptions() {
    return this.productOptions;
  }

  addOption(option: string, optionValue: Array<string>) {
    this.productOptions.set(option, optionValue);
  }

  deleteOption(option: string) {
    const optionIndex = this.productOptions.get(option);
    if (optionIndex === null) return;
    this.productOptions.delete(option);
  }

  deleteOptionvalue(option: string, optionValue: string) {
    const optionValues = this.productOptions.get(option);
    if (optionValues) {
      const index = optionValues.indexOf(optionValue);
      if (index > -1) {
        optionValues.splice(index, 1);
      }
    }
  }
}
