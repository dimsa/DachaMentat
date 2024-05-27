import { Component, Input } from '@angular/core';
import { AdminSensorDto } from '../dto/AdminSensorDto';
import { SensorService } from '../services/sensor.service';
import { CoordinatesDto } from '../dto/CoordinatesDto';

@Component({
  selector: '[app-admin-sensor-meta-item]',
  templateUrl: './admin-sensor-meta-item.component.html',
  styleUrls: ['./admin-sensor-meta-item.component.css']
})
export class AdminSensorMetaItemComponent {
  @Input() public data: AdminSensorDto = new AdminSensorDto("0", "0", "0", new CoordinatesDto(), "0");

  isEditing: boolean = false;

  /*get textCoordinates(): string {
    return this.data.coordinates.toString();
  }

  set textCoordinates(value: string) {
    this.data.coordinates = new CoordinatesDto(value);
  }*/

  get latitude(): number {
    return this.data.coordinates.latitude;
  }

  set latitude(value: number) {
    this.data.coordinates.latitude = value;
  }

  get longitude(): number {
    return this.data.coordinates.longitude;
  }

  set longitude(value: number) {
    this.data.coordinates.longitude = value;
  }



  constructor(private sensorService: SensorService) {
    console.log(sensorService);
  }

  startEdit(): void {
    this.isEditing = true;
  }

  cancelEdit(): void {
    this.isEditing = false;
  }

  async updateData(): Promise<boolean> {
    let res = await this.sensorService.updateSensor(this.data);
    if (res === true) {
      this.data.onUpdate.emit(this.data);
    }
    return res;
  }
}
