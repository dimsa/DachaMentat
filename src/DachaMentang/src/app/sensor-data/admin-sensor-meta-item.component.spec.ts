import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSensorMetaItem } from './admin-sensor-meta-item.component';

describe('SensorDataComponent', () => {
  let component: AdminSensorMetaItem;
  let fixture: ComponentFixture<AdminSensorMetaItem>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminSensorMetaItem]
    });
    fixture = TestBed.createComponent(AdminSensorMetaItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
