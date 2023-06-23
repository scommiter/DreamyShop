import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import * as moment from 'moment';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { UserService } from 'src/app/services/user.service';
import { UserDto, UserUpdateDto } from 'src/app/shared/models/user-dto';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss'],
})
export class UserEditComponent implements OnInit {
  public formUserUpdate: FormGroup;
  gender: string = '';
  avatar: string = '';
  user: UserDto = {
    id: 0,
    fullName: '',
    genderType: true,
    dob: new Date(),
    avatar: '',
    email: '',
    phone: '',
    address: '',
    roleTypes: [],
  };

  userUpdateDto: UserUpdateDto = {
    fullName: '',
    genderType: true,
    dob: new Date(),
    avatar: '',
    phone: '',
    email: '',
    identityID: '',
    address: '',
    occupation: '',
    country: '',
    profileUrl: '',
  };

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private ref: DynamicDialogRef
  ) {
    this.formUserUpdate = this.fb.group({
      fullName: new FormControl('', [
        Validators.minLength(3),
        Validators.maxLength(20),
        Validators.required,
      ]),
      gender: new FormControl(''),
      dob: new FormControl(''),
      avatar: new FormControl(''),
      email: new FormControl(''),
      phone: new FormControl(''),
      identityID: new FormControl(''),
      address: new FormControl(''),
      occupation: new FormControl(''),
      country: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.user = this.userService.getUser();
    this.avatar = this.user.avatar;
    this.formUserUpdate.setValue({
      fullName: this.user.fullName,
      gender: this.user.genderType == true ? 'male' : 'female',
      dob: moment(this.user.dob).format('YYYY-MM-DD'),
      avatar: this.user.avatar,
      email: this.user.email,
      phone: this.user.phone,
      identityID: '',
      address: this.user.address,
      occupation: '',
      country: '',
    });
  }

  url: string[] = [''];
  onSelectFile(event: any) {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0] as File;
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url
      reader.onload = (event) => {
        // called once readAsDataURL is completed
        this.avatar = reader.result as string;
        this.formUserUpdate.patchValue({
          avatar: reader.result as string,
        });
        this.url.push(this.avatar);
      };
    }
  }

  convertToUserUpdateDto() {
    this.userUpdateDto.fullName = this.formUserUpdate.get('fullName')?.value;
    this.userUpdateDto.genderType =
      this.formUserUpdate.get('gender')?.value == 'male' ? true : false;
    this.userUpdateDto.dob = this.formUserUpdate.get('dob')?.value;
    this.userUpdateDto.avatar = this.formUserUpdate.get('avatar')?.value;
    this.userUpdateDto.phone = this.formUserUpdate.get('phone')?.value;
    this.userUpdateDto.email = this.formUserUpdate.get('email')?.value;
    this.userUpdateDto.identityID =
      this.formUserUpdate.get('identityID')?.value;
    this.userUpdateDto.address = this.formUserUpdate.get('address')?.value;
    this.userUpdateDto.occupation =
      this.formUserUpdate.get('occupation')?.value;
    this.userUpdateDto.country = this.formUserUpdate.get('country')?.value;
  }

  saveChange() {
    this.convertToUserUpdateDto();
    console.log('this.formUserUpdate:>> ', this.userUpdateDto);
    this.userService.updateUser(this.user.id, this.userUpdateDto).subscribe({
      next: () => {
        this.ref.close(this);
      },
      error: (err) => {},
    });
  }
}
