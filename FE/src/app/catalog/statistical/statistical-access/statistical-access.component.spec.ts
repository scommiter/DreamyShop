import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticalAccessComponent } from './statistical-access.component';

describe('StatisticalAccessComponent', () => {
  let component: StatisticalAccessComponent;
  let fixture: ComponentFixture<StatisticalAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatisticalAccessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatisticalAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
