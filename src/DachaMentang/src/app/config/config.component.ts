import { Component } from '@angular/core';
import { AdminSensorDto } from '../dto/AdminSensorDto';
import { LoggerService } from '../services/logger.service';
import { SensorService } from '../services/sensor.service';
import { CoordinatesDto } from '../dto/CoordinatesDto';


@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.css']
})
export class ConfigComponent {
  public sensors: Array<AdminSensorDto> = new Array<AdminSensorDto>;

  public addSensor(): void {
    var addSensor = this.sensorConfig.addEmptySensor();
    addSensor.then((res: boolean) => {
      this.fetchData();
     })
      .catch((error: any) => {
        this.logger.error(error);
        throw error;
      });
  }

  ngOnInit() {
    this.fetchData();
  }


  private fetchData(): void {
  var loadedSensors = this.sensorConfig.fetchAdminSensors();

  loadedSensors
    .then((res: AdminSensorDto[]) => {
      this.sensors = res;

      for (var i = 0; i < res.length; i++) {
        res[i].onUpdate.on((newData) => { this.fetchData(); })
      }
    })
    .catch((error: any) => {
      this.logger.error(error);
      throw error;
    });
}

  public constructor(private sensorConfig: SensorService, private logger: LoggerService) {
    this.sensors = [];
      /*new AdminSensorDto("1", "fdsf54jytmn45", "MainTemp", new CoordinatesDto(), "°C"),
      new AdminSensorDto("2", "m67456ghtrggg", "MainHumidity", new CoordinatesDto(), "%"),
      new AdminSensorDto("3", "fc453tvy63673", "MainRaon", new CoordinatesDto(), "Rain")
    ];*/
  }
}
