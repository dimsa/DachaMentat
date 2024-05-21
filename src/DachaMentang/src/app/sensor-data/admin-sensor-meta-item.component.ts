import { Component, Input } from '@angular/core';
import { AdminSensorDto } from '../dto/AdminSensorDto';
import { SensorConfigService } from '../services/sensor-config.service';

@Component({
  selector: '[app-admin-sensor-meta-item]',
  templateUrl: './admin-sensor-meta-item.component.html',
  styleUrls: ['./admin-sensor-meta-item.component.css']
})
export class AdminSensorMetaItemComponent {
  @Input() public data: AdminSensorDto = new AdminSensorDto("0", "0", "0", "0", "0");
  isEditing: boolean = false;

  constructor(private sensorConfig: SensorConfigService) {
    console.log(sensorConfig);
  }

  startEdit(): void {
    this.isEditing = true;
  }

  cancelEdit(): void {
    this.isEditing = false;
  }

  updateData(): boolean {
    return false;
  }
}
