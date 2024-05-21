import { Component } from '@angular/core';
import { AdminSensorDto } from '../dto/AdminSensorDto';
import { AdminSensorMetaItemComponent } from '../sensor-data/admin-sensor-meta-item.component';


@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.css']
})
export class ConfigComponent {
  public sensors: Array<AdminSensorDto> = new Array<AdminSensorDto>;

  public addSensor(): void {

  }

  public constructor() {
    this.sensors = [
      new AdminSensorDto("1", "fdsf54jytmn45", "MainTemp", "95.23213, 34.43432", "°C"),
      new AdminSensorDto("2", "m67456ghtrggg", "MainHumidity", "95.23213, 34.43432", "%"),
      new AdminSensorDto("3", "fc453tvy63673", "MainRaon", "95.23213, 34.43432", "Rain")
    ];
  }
}
