import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSensorMetaItemComponent } from './admin-sensor-meta-item.component';

describe('SensorDataComponent', () => {
  let component: AdminSensorMetaItemComponent;
  let fixture: ComponentFixture<AdminSensorMetaItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminSensorMetaItemComponent]
    });
    fixture = TestBed.createComponent(AdminSensorMetaItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
