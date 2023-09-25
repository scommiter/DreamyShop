import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AppMenuComponent } from 'src/app/layout/app.menu.component';
import { AuthService } from 'src/app/services/auth.service';

@Directive({
  selector: '[appRoleCheck]'
})
export class HasRoleDirective implements OnInit {

  private _role: number | undefined;

  @Input() set appRoleCheck(value: number | undefined) {
    this._role = value;
    this.applyRoleLogic();
  }

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private jwtHelper: JwtHelperService,
    private authService: AuthService
    ) {}

  private applyRoleLogic() {
    const token = this.authService.getToken();
    if(token != null){
      const decodedToken = this.jwtHelper.decodeToken(token as string);
      const userRoles = decodedToken.RoleTypes;
      console.log('this._role :>> ', this._role);
      console.log('this.userRoles :>> ', userRoles);
      if (userRoles.includes(this._role)) {
        //create view component
        this.viewContainer.createEmbeddedView(this.templateRef);
      } else {
        //destroy component 
        this.viewContainer.clear();
      }
    } else {
      //destroy component 
      this.viewContainer.clear();
    }
  }

  ngOnInit() {
     
  }
}

