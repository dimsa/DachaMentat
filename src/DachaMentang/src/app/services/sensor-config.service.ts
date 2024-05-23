import { Injectable } from '@angular/core';
import { WebClientService } from './web-client.service';
import { LoggerService } from './logger.service';
import { ApiSchemeService } from './api-scheme.service';
import { GuestSensorDto } from '../dto/GuestSensorDto';

@Injectable({
  providedIn: 'root'
})
export class SensorConfigService {
  async fetchSensors(): Promise<GuestSensorDto[]> {

    return await this.httpClient.get<GuestSensorDto[]>(this.apiScheme.getSensorsUrl());
  /*  sensorsGet
      .then((res: SensorViewDto[]) => {
        return res;
        //this.httpClient.setAuth(res.token);
        //this.logger.info(res);
      })
      .catch((error: any) => {
        throw error();
        //console.error('Promise rejected with error: ' + error);
        return undefined;
      });*/

  }

  constructor(private httpClient: WebClientService, private apiScheme: ApiSchemeService, private logger: LoggerService) {
  }
}
