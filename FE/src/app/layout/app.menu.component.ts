import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService) {}

  ngOnInit() {
    this.model = [
      {
        label: 'Tổng quan',
        items: [
          {
            label: 'Bảng tổng quan',
            icon: 'pi pi-fw pi-home',
            routerLink: ['/admin/dash-board'],
          },
        ],
      },
      {
        label: 'Sản phẩm',
        items: [
          {
            label: 'Quản lý sản phẩm',
            icon: 'pi pi-check-circle',
            routerLink: ['/product'],
          },
        ],
      },
      {
        label: 'Thống kê',
        items: [
          {
            label: 'Lượng mua bán',
            icon: 'pi pi-fw pi-chart-bar',
            routerLink: ['/statistical/sales-volume'],
          },
          {
            label: 'Lượng truy cập',
            icon: 'pi pi-fw pi-globe',
            routerLink: ['/statistical/access-volume'],
          },
        ],
      },
      {
        label: 'Quản lý shop',
        items: [
          {
            label: 'Người dùng',
            icon: 'pi pi-fw pi-user',
            routerLink: ['/utilities/icons'],
          },
        ],
      },
      {
        label: 'Kho',
        items: [
          {
            label: 'Phiếu nhập hàng',
            icon: 'pi pi-fw pi-ticket',
            routerLink: ['/utilities/icons'],
          },
          {
            label: 'Tồn kho',
            icon: 'pi pi-fw pi-server',
            routerLink: ['/utilities/icons'],
          },
        ],
      },
      {
        label: 'Đơn hàng',
        items: [
          {
            label: 'Quản lý đơn hàng',
            icon: 'pi pi-fw pi-tablet',
            routerLink: ['/utilities/icons'],
          },
        ],
      },
    ];
  }
}
