import { Component } from '@angular/core';
import { SensorService } from '../services/sensor.service';
import { LoggerService } from '../services/logger.service';
import { ActivatedRoute } from '@angular/router';
import { CoordinatesDto } from '../dto/CoordinatesDto';
import { SensorMetaDto } from '../dto/SensorMetaDto';
import { IndicationDto } from '../dto/IndicationDto';
import { EChartsOption } from 'echarts';
import { ChartMesher } from '../chart-mesher/ChartMesher';

@Component({
  selector: 'app-sensor',
  templateUrl: './sensor.component.html',
  styleUrls: ['./sensor.component.css']
})
export class SensorComponent {
  sensorId: number;

  sensorName: string = "Unknown";

  sensorCoordinates: CoordinatesDto = new CoordinatesDto("0, 0");

  sensorUnitOfMeasure: string = "Unknown";

  indications: IndicationDto[] = [];

  public constructor(private route: ActivatedRoute, private sensorConfig: SensorService, private logger: LoggerService) {
    /* this.sensors = [
       new GuestSensorDto("1", "MainTemp",     new CoordinatesDto(), "°C", "0", new Date().toLocaleString()),
       new GuestSensorDto("2", "MainHumidity", new CoordinatesDto(), "%", "0", new Date().toLocaleString()),
       new GuestSensorDto("3", "MainRaon",     new CoordinatesDto(), "Chance", "0", new Date().toLocaleString())
     ];*/
    //    let a = this.route.snapshot.url[0].parameters;
    let id = this.route.snapshot.url[1] as unknown;
    this.sensorId = id as number;
  }

  chartOption: EChartsOption = {
    xAxis: {
      type: 'category',
      data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
    },
    yAxis: {
      type: 'value',
    },
    series: [
      {
        data: [820, 932, 901, 934, 1290, 1330, 1320],
        type: 'line',
      },
    ],
  };

  loading = true;

  ngOnInit() {
    var loadedSensor = this.sensorConfig.fetchGuestSensor(this.sensorId);
  
    loadedSensor
      .then((res: SensorMetaDto) => {
        //this.sensors = res;
        this.sensorName = res.name;
        this.sensorCoordinates = new CoordinatesDto()
        this.sensorCoordinates.latitude = res.coordinates.latitude;
        this.sensorCoordinates.longitude = res.coordinates.longitude;
        this.sensorUnitOfMeasure = res.unitOfMeasure;
        this.indications = [];
        for (var i = 0; i < res.indications.length; i++) {
          var ind = new IndicationDto(res.indications[i].value.toString(), res.indications[i].timeStamp)
          this.indications.push(ind);
        }
        this.indications = res.indications;


        let mesher = new ChartMesher(this.indications);

        let timeStamps = mesher.getTimeStamps();
        let series = mesher.getSeries();

        this.chartOption = {
          xAxis: {
            type: 'category',
            data: timeStamps as any,
            axisLabel: {
              show: true,
              interval: 0,
              rotate: 90,
            },
          },
          yAxis: {
            type: 'value',
          },
          series: [
            {
              data: series,
              type: 'line',
            },
          ],
        };

        /*if (this.chartOption.xAxis) {
          this.chartOption.xAxis.data = ;  
        }

        this.chartOption.series.data = ;*/

        this.loading = false;
        })
        .catch((error: any) => {
          this.logger.error(error);
          throw error();
        });
  }

}
