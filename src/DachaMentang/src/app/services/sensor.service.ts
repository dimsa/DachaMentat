import { Injectable } from '@angular/core';
import { WebClientService } from './web-client.service';
import { LoggerService } from './logger.service';
import { ApiSchemeService } from './api-scheme.service';
import { GuestSensorDto } from '../dto/GuestSensorDto';
import { AdminSensorDto } from '../dto/AdminSensorDto';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
  async addEmptySensor(): Promise<boolean> {
    return await this.httpClient.get<boolean>(this.apiScheme.addSensorsUrl());
  }
  async fetchGuestSensors(): Promise<GuestSensorDto[]> {

    return await this.httpClient.get<GuestSensorDto[]>(this.apiScheme.getSensorsUrl());
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
