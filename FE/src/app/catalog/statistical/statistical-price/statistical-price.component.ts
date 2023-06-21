import { Component, OnDestroy } from '@angular/core';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexLegend,
  ApexStroke,
  ApexXAxis,
  ApexFill,
  ApexTooltip,
} from 'ng-apexcharts';
import { Subject, takeUntil } from 'rxjs';
import { ChartService } from 'src/app/services/chart.service';
import {
  ChartMonthlySaleDto,
  PricePaymentTypeDto,
} from 'src/app/shared/models/chart-weekly-sale-dto';

export type ChartPriceOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
};

export type ChartProfitOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  tooltip: ApexTooltip;
  stroke: ApexStroke;
};

@Component({
  selector: 'app-statistical-price',
  templateUrl: './statistical-price.component.html',
  styleUrls: ['./statistical-price.component.scss'],
})
export class StatisticalPriceComponent implements OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  public chartPriceOptions: ChartPriceOptions = {
    series: [
      {
        name: 'Tháng hiện tại',
        data: [0, 0, 0, 0],
      },
      {
        name: 'Tháng trước',
        data: [0, 0, 0, 0],
      },
    ],
    chart: {
      height: 350,
      type: 'area',
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      curve: 'smooth',
    },
    xaxis: {
      type: 'category',
      categories: ['Tuần 1', 'Tuần 2', 'Tuần 3', 'Tuần 4'],
    },
    tooltip: {
      x: {
        format: 'dd/MM/yy HH:mm',
      },
    },
  };
  public chartProfitOptions: ChartProfitOptions = {
    series: [
      {
        name: 'Mục tiêu',
        data: [44, 55, 57, 56, 61, 58, 63, 60, 66, 55, 23, 45],
      },
      {
        name: 'Doanh thu thực tế',
        data: [76, 85, 101, 98, 87, 105, 91, 114, 94, 23, 54, 34],
      },
    ],
    chart: {
      type: 'bar',
      height: 350,
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: '55%',
      },
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 2,
      colors: ['transparent'],
    },
    xaxis: {
      categories: [
        'Tháng 1',
        'Tháng 2',
        'Tháng 3',
        'Tháng 4',
        'Tháng 5',
        'Tháng 6',
        'Tháng 7',
        'Tháng 8',
        'Tháng 9',
        'Tháng 10',
        'Tháng 11',
        'Tháng 12',
      ],
    },
    yaxis: {
      title: {
        text: 'VND',
      },
    },
    fill: {
      opacity: 1,
    },
    tooltip: {
      y: {
        formatter: function (val) {
          return val.toLocaleString('en-US', {
            style: 'currency',
            currency: 'VND',
          });
        },
      },
    },
  };
  dataLoaded: boolean = false;
  constructor(private chartService: ChartService) {
    this.getDataChartMonthlySale();
    this.getDataPricePaymentType();
  }

  getDataChartMonthlySale() {
    this.chartService
      .getChartMonthlySale()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ChartMonthlySaleDto) => {
          this.chartPriceOptions.series[0].data =
            response.percentOfSalesCurrentMonth;
          this.chartPriceOptions.series[1].data =
            response.percentOfSalesLastMonth;
          this.dataLoaded = true;
        },
        error: () => {},
      });
  }

  totalPrice: any;
  cash: any;
  banking: any;
  visaMasterCard: any;
  getDataPricePaymentType() {
    this.chartService
      .getPricePaymentType()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PricePaymentTypeDto) => {
          this.totalPrice = response.totalPrices.toLocaleString('en-US', {
            style: 'currency',
            currency: 'VND',
          });
          this.cash = response.cash.toLocaleString('en-US', {
            style: 'currency',
            currency: 'VND',
          });
          this.banking = response.banking.toLocaleString('en-US', {
            style: 'currency',
            currency: 'VND',
          });
          this.visaMasterCard = response.visaMasterCard.toLocaleString(
            'en-US',
            {
              style: 'currency',
              currency: 'VND',
            }
          );
        },
        error: () => {},
      });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
