import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexFill,
  ApexNonAxisChartSeries,
  ApexPlotOptions,
  ApexResponsive,
  ApexTitleSubtitle,
  ApexXAxis,
  ApexYAxis,
  ChartComponent,
} from 'ng-apexcharts';
import { Subject, takeUntil } from 'rxjs';
import { ChartService } from 'src/app/services/chart.service';
import { ChartWeeklySaleDto } from 'src/app/shared/models/chart-weekly-sale-dto';

export type ChartSaleVolumns = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  title: ApexTitleSubtitle;
};

export type ChartCircles = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-admin-dash-board',
  templateUrl: './admin-dash-board.component.html',
  styleUrls: ['./admin-dash-board.component.scss'],
})
export class AdminDashBoardComponent implements OnInit, OnDestroy {
  @ViewChild('chart', { static: false })
  chart!: ChartComponent;
  private ngUnsubscribe = new Subject<void>();
  public chartSaleVolumns: ChartSaleVolumns;
  public chartCircles: ChartCircles;
  constructor(private chartService: ChartService) {
    chartService
      .getChartWeeklySale()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ChartWeeklySaleDto) => {
          this.chartSaleVolumns.series.map(
            (p) => (p.data = response.percentOfSalesByDay)
          );
          console.log(
            'this.chartSaleVolumns.series :>> ',
            this.chartSaleVolumns.series
          );
        },
        error: () => {},
      });
    this.chartSaleVolumns = {
      series: [
        {
          name: 'Inflation',
          data: [0, 0, 0, 0, 0, 0, 0],
        },
      ],
      chart: {
        height: 350,
        type: 'bar',
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: 'top', // top, center, bottom
          },
        },
      },
      dataLabels: {
        enabled: true,
        formatter: function (val) {
          return val + '%';
        },
        offsetY: -20,
        style: {
          fontSize: '12px',
          colors: ['#304758'],
        },
      },

      xaxis: {
        categories: ['MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT', 'SUN'],
        position: 'top',
        labels: {
          offsetY: -18,
        },
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        crosshairs: {
          fill: {
            type: 'gradient',
            gradient: {
              colorFrom: '#D8E3F0',
              colorTo: '#BED1E6',
              stops: [0, 100],
              opacityFrom: 0.4,
              opacityTo: 0.5,
            },
          },
        },
        tooltip: {
          enabled: true,
          offsetY: -35,
        },
      },
      fill: {
        type: 'gradient',
        gradient: {
          shade: 'light',
          type: 'horizontal',
          shadeIntensity: 0.25,
          gradientToColors: undefined,
          inverseColors: true,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [50, 0, 100, 100],
        },
      },
      yaxis: {
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        labels: {
          show: false,
          formatter: function (val) {
            return val + '%';
          },
        },
      },
      title: {
        text: 'Sơ đồ lượng bán tuần vừa qua',
        floating: true,
        offsetY: 320,
        align: 'center',
        margin: 25,
        style: {
          color: '#444',
          fontWeight: 700,
        },
      },
    };
    this.chartCircles = {
      series: [44, 55, 13, 43, 22],
      chart: {
        width: 380,
        type: 'pie',
      },
      labels: ['Thời trang', 'Đồ điện tử', 'Đồ ăn', 'Đồ gia dụng', 'Khác'],
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200,
            },
            legend: {
              position: 'bottom',
            },
          },
        },
      ],
    };
    console.log('aaaaaaaaaaaaaaaaaaaa :>> ', this.chartSaleVolumns.series);
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
