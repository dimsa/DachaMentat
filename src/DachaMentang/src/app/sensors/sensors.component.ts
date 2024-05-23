import { Component } from '@angular/core';
import { GuestSensorDto } from '../dto/GuestSensorDto';
import { SensorConfigService } from '../services/sensor-config.service';

@Component({
  selector: 'app-sensors',
  templateUrl: './sensors.component.html',
  styleUrls: ['./sensors.component.css']
})
export class SensorsComponent {
  public sensors: Array<GuestSensorDto> = new Array<GuestSensorDto>;

  ngOnInit() {
    var loadedSensors = this.sensorConfig.fetchSensors();

    loadedSensors
      .then((res: GuestSensorDto[]) => {
        this.sensors = res;
      //this.httpClient.setAuth(res.token);
      //this.logger.info(res);
    })
    .catch((error: any) => {
      throw error();
      //console.error('Promise rejected with error: ' + error);
      return undefined;
    });
  }

  public constructor(private sensorConfig: SensorConfigService) {
    this.sensors = [
      new GuestSensorDto("1", "MainTemp", "95.23213, 34.43432", "°C", "0", new Date().toLocaleString()),
      new GuestSensorDto("2", "MainHumidity", "95.23213, 34.43432", "%", "0", new Date().toLocaleString()),
      new GuestSensorDto("3", "MainRaon", "95.23213, 34.43432", "Rain", "0", new Date().toLocaleString())
    ];
  }
}
