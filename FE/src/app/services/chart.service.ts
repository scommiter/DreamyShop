import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { Observable } from 'rxjs';
import { ChartWeeklySaleDto } from '../shared/models/chart-weekly-sale-dto';
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
}
