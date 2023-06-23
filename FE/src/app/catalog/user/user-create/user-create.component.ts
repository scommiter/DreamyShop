import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.scss'],
})
export class UserCreateComponent {
  public formUserCreate: FormGroup;

  constructor(private fb: FormBuilder) {
    this.formUserCreate = this.fb.group({
      name: new FormControl('', [
        Validators.minLength(3),
        Validators.maxLength(20),
        Validators.required,
      ]),
      code: new FormControl('', [
        Validators.minLength(3),
        Validators.maxLength(10),
        Validators.required,
      ]),
      description: new FormControl('', [Validators.maxLength(200)]),
      productType: new FormControl('', [Validators.required]),
      categoryName: new FormControl('', [Validators.required]),
      manufacturerName: new FormControl('', [Validators.required]),
      isActive: new FormControl('True'),
      isVisibily: new FormControl('True'),
      sku: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required]),
      quantity: new FormControl('', [Validators.required]),
    });
  }

  saveChange() {}
}
