import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { UserService } from 'src/app/services/user.service';
import { UserDto } from 'src/app/shared/models/user-dto';

@Component({
  selector: 'app-assign-role',
  templateUrl: './assign-role.component.html',
  styleUrls: ['./assign-role.component.scss'],
})
export class AssignRoleComponent implements OnInit {
  userName: string = '';
  selectedRoles: any[] = [];
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

  constructor(
    private userService: UserService,
    private ref: DynamicDialogRef,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.user = this.userService.getUser();
    this.userName = this.user.fullName;
    let roles = this.roles.filter((r) => this.user.roleTypes.includes(r.key));
    roles.forEach((r) => this.selectedRoles.push(r));
  }

  roles: any[] = [
    { name: 'Administrator', key: 1 },
    { name: 'Employee', key: 2 },
    { name: 'Customer', key: 3 },
  ];

  assignRole() {
    console.log(
      'this.selectedRoles :>> ',
      this.selectedRoles.map((role) => role.key)
    );
    if (this.user.roleTypes.length == 0) {
      this.userService
        .assignRole(
          this.user.id,
          this.selectedRoles.map((role) => role.key)
        )
        .subscribe({
          next: () => {
            this.ref.close(this);
            this.show();
          },
          error: (err) => {},
        });
    } else {
      this.userService
        .updateRole(
          this.user.id,
          this.selectedRoles.map((role) => role.key)
        )
        .subscribe({
          next: () => {
            this.ref.close(this);
            this.show();
          },
          error: (err) => {},
        });
    }
  }
  show() {
    this.messageService.add({
      severity: 'success',
      summary: 'Thành công',
      detail: 'Cập nhật sản phẩm thành công',
    });
  }
}
