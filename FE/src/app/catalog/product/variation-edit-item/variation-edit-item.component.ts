import { Component, ElementRef, Input, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-variation-edit-item',
  templateUrl: './variation-edit-item.component.html',
  styleUrls: ['./variation-edit-item.component.scss'],
})
export class VariationEditItemComponent {
  @Input() productOptionKey: string = '';
  @Input() productOptionValue: string[] = [];
  @Input() indexProductOption: number = 1;

  inputs: { value: string }[] = [{ value: '' }];
  inputValues: string[] = [''];

  constructor(private renderer: Renderer2, private el: ElementRef) {}

  onInputFocus() {
    if (
      this.inputs.length === 0 ||
      this.inputs[this.inputs.length - 1].value !== ''
    ) {
      this.inputs.push({ value: '' });
    }
  }
}
