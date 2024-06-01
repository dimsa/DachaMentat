import { Injectable } from '@angular/core';
import { WebClientService } from './web-client.service';
import { LoggerService } from './logger.service';
import { ApiSchemeService } from './api-scheme.service';
import { GuestSensorDto } from '../dto/GuestSensorDto';
import { AdminSensorDto } from '../dto/AdminSensorDto';
import { SensorMetaDto } from '../dto/SensorMetaDto';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
  async fetchGuestSensor(sensorId: number): Promise<SensorMetaDto> {

    let rawRes = await this.httpClient.get<SensorMetaDto>(this.apiScheme.getSensorUrl(sensorId.toString()));
   /* let res = new Array<SensorMetaDto>;
    for (var i = 0; i < rawRes.length; i++) {
      let dto = new SensorMetaDto(
        rawRes[i].id,
        rawRes[i].name,
        rawRes[i].coordinates,
        rawRes[i].unitOfMeasure,
        rawRes[i].indications);
      res.push(dto);
    }*/

    return rawRes;
  }
  async updateSensor(sensorData: AdminSensorDto): Promise<boolean> {
    let res = await this.httpClient.post<boolean>(this.apiScheme.updateSensorUrl(sensorData.id), sensorData);

    return res;
  }

  async addEmptySensor(): Promise<boolean> {
    return await this.httpClient.get<boolean>(this.apiScheme.addSensorsUrl());
  }

  async fetchGuestSensors(): Promise<GuestSensorDto[]> {

    let rawRes = await this.httpClient.get<GuestSensorDto[]>(this.apiScheme.getSensorsUrl());
    let res = new Array<GuestSensorDto>;
    for (var i = 0; i < rawRes.length; i++) {
      let dto = new GuestSensorDto(
        rawRes[i].id,
        rawRes[i].name,
        rawRes[i].coordinates,
        rawRes[i].unitOfMeasure,
        rawRes[i].lastIndication,
        rawRes[i].lastIndicationTimeStamp);
      res.push(dto);
    }

    return res;
  }

  async fetchAdminSensors(): Promise<AdminSensorDto[]> {
    let rawRes = await this.httpClient.get<AdminSensorDto[]>(this.apiScheme.getAdminSensorsUrl());
    let res = new Array<AdminSensorDto>;
    for (var i = 0; i < rawRes.length; i++) {
      let dto = new AdminSensorDto(rawRes[i].id, rawRes[i].privateKey, rawRes[i].name, rawRes[i].coordinates, rawRes[i].unitOfMeasure);
      res.push(dto);
    }

    return res;
  }

  constructor(private httpClient: WebClientService, private apiScheme: ApiSchemeService, private logger: LoggerService) {
  }
}
