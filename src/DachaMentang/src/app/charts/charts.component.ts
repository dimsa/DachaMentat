import { Component } from '@angular/core';
import { EChartsOption } from 'echarts';
import { SensorService } from '../services/sensor.service';
import { LoggerService } from '../services/logger.service';
import { SensorMetaDto } from '../dto/SensorMetaDto';
import { CoordinatesDto } from '../dto/CoordinatesDto';
import { ChartData } from '../model/ChartData';
import { IndicationDto } from '../dto/IndicationDto';
import { ChartMesher } from '../model/ChartMesher';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent {

  public sensorsToShow: ChartData[] = new Array<ChartData>();

  chartOption: EChartsOption = {
    xAxis: {
      type: 'category',
      data: ['None'],
    },
    yAxis: {
      type: 'value',
    },
    series: [
      {
        data: [42],
        type: 'line',
      },
    ],
  };


  public constructor(private sensorConfig: SensorService, private logger: LoggerService) {

  }

  public get loading() {
    let res = true;

    for (var i = 0; i < this.sensorsToShow.length; i++) {
      res = res && this.sensorsToShow[i].loaded;
    }

    return !res;
  }

  ngOnInit() {

    let currentTime = Date.now();
    for (var sensorId = 1; sensorId < 5; sensorId++) {
      var loadedSensor = this.sensorConfig.fetchGuestSensor(sensorId);


      loadedSensor
        .then((res: SensorMetaDto) => {
          //this.sensors = res;
          let sensorCoordinates = new CoordinatesDto()
          sensorCoordinates.latitude = res.coordinates.latitude;
          sensorCoordinates.longitude = res.coordinates.longitude;

          let sensor = new ChartData(sensorId.toString(), res.name, sensorCoordinates, res.unitOfMeasure);

          let indications = [];
          for (var i = 0; i < res.indications.length; i++) {
            var ind = new IndicationDto(res.indications[i].value.toString(), res.indications[i].timeStamp)
            indications.push(ind);
          }

          let mesher = new ChartMesher(indications, currentTime);

          sensor.timeStamps = mesher.getTimeStamps();
          sensor.series = mesher.getSeries();

          this.sensorsToShow.push(sensor);
          sensor.loaded = true;

          this.checkFullyLoad();
        })
        .catch((error: any) => {
          this.logger.error(error);
          throw error();
        });
    }
    }
  checkFullyLoad() {
    if (this.sensorsToShow.length == 4) {
      if (!this.loading) {
        this.prepare();
      }
    }
  }


  prepare() {
    let baseChart = this.sensorsToShow[0];

      this.chartOption = {
      xAxis: {
        type: 'category',
          data: baseChart.timeStamps as any,
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
          data: this.sensorsToShow[0].series,
          type: 'line',
        }, {
          data: this.sensorsToShow[1].series,
          type: 'line',
        },
        {
          data: this.sensorsToShow[2].series,
          type: 'line',
        },
        {
          data: this.sensorsToShow[3].series,
          type: 'line',
        },
      ],
    };
  }

}
