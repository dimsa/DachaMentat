import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestSensorMetaItemComponent } from './guest-sensor-meta-item.component';

describe('GuestSensorMetaItemComponent', () => {
  let component: GuestSensorMetaItemComponent;
  let fixture: ComponentFixture<GuestSensorMetaItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuestSensorMetaItemComponent]
    });
    fixture = TestBed.createComponent(GuestSensorMetaItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
