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

  addClassifyProductVariant() {
    this.addClassifyProduct = true;
    this.productOptions.push({ key: '', value: [''] });
  }

  onInputOptionValue(index: number, value: string, indexChild: number) {
    if (
      value !== '' &&
      indexChild === this.productOptions[index].value.length - 1
    ) {
      this.productOptions[index].value.push('');
      this.productVariants[index].thumbnail_picture.push('');
      this.isVisibility.push(true);
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
