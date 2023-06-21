export interface ChartWeeklySaleDto {
  percentOfSalesByDay: number[];
}

export interface ChartMonthlySaleDto {
  percentOfSalesCurrentMonth: number[];
  percentOfSalesLastMonth: number[];
}

export interface PricePaymentTypeDto {
  totalPrices: number;
  cash: number;
  banking: number;
  visaMasterCard: number;
}

export interface StatisticDashboardDto {
  numberNewOrders: number;
  numberCustomers: number;
  totalPrices: number;
}
