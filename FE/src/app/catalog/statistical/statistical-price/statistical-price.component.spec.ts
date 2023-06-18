import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticalPriceComponent } from './statistical-price.component';

describe('StatisticalPriceComponent', () => {
  let component: StatisticalPriceComponent;
  let fixture: ComponentFixture<StatisticalPriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatisticalPriceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatisticalPriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
