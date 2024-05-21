import { Component } from '@angular/core';
import { GuestSensorDto } from '../dto/GuestSensorDto';

@Component({
  selector: 'app-sensors',
  templateUrl: './sensors.component.html',
  styleUrls: ['./sensors.component.css']
})
export class SensorsComponent {
  public sensors: Array<GuestSensorDto> = new Array<GuestSensorDto>;
  public constructor() {
    this.sensors = [
      new GuestSensorDto("1", "MainTemp", "95.23213, 34.43432", "°C", new Date().toLocaleString()),
      new GuestSensorDto("2", "MainHumidity", "95.23213, 34.43432", "%", new Date().toLocaleString()),
      new GuestSensorDto("3", "MainRaon", "95.23213, 34.43432", "Rain", new Date().toLocaleString())
    ];
  }
}
