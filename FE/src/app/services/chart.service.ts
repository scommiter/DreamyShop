import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { Observable } from 'rxjs';
import {
  ChartMonthlySaleDto,
  ChartWeeklySaleDto,
  PricePaymentTypeDto,
  StatisticDashboardDto,
} from '../shared/models/chart-weekly-sale-dto';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChartService {
  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) {}

  public getChartWeeklySale(): Observable<ChartWeeklySaleDto> {
    return this.http.get<ChartWeeklySaleDto>(
      `${environment.apiUrl}/api/Chart/getChartSalesWeekly`
    );
  }

  public getStatisticDashboard(): Observable<StatisticDashboardDto> {
    return this.http.get<StatisticDashboardDto>(
      `${environment.apiUrl}/api/Chart/getStatisticDashboard`
    );
  }

  public getChartMonthlySale(): Observable<ChartMonthlySaleDto> {
    return this.http.get<ChartMonthlySaleDto>(
      `${environment.apiUrl}/api/Chart/getChartMonthlySale`
    );
  }

  public getPricePaymentType(): Observable<PricePaymentTypeDto> {
    return this.http.get<PricePaymentTypeDto>(
      `${environment.apiUrl}/api/Chart/getPricePaymentType`
    );
  }
}
