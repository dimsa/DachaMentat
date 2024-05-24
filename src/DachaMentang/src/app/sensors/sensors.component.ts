import { Component } from '@angular/core';
import { GuestSensorDto } from '../dto/GuestSensorDto';
import { SensorService } from '../services/sensor.service';
import { LoggerService } from '../services/logger.service';
import { CoordinatesDto } from '../dto/CoordinatesDto';

@Component({
  selector: 'app-sensors',
  templateUrl: './sensors.component.html',
  styleUrls: ['./sensors.component.css']
})
export class SensorsComponent {
  public sensors: Array<GuestSensorDto> = new Array<GuestSensorDto>;

  ngOnInit() {
    var loadedSensors = this.sensorConfig.fetchGuestSensors();

    loadedSensors
      .then((res: GuestSensorDto[]) => {
        this.sensors = res;
    })
      .catch((error: any) => {
        this.logger.error(error);
        throw error();      
    });
  }

  public constructor(private sensorConfig: SensorService, private logger: LoggerService) {
   /* this.sensors = [
      new GuestSensorDto("1", "MainTemp",     new CoordinatesDto(), "°C", "0", new Date().toLocaleString()),
      new GuestSensorDto("2", "MainHumidity", new CoordinatesDto(), "%", "0", new Date().toLocaleString()),
      new GuestSensorDto("3", "MainRaon",     new CoordinatesDto(), "Chance", "0", new Date().toLocaleString())
    ];*/
  }
}
