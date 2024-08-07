import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiSchemeService {
  getSensorUrl(id: string): string {
    return this.baseUrl + "sensor/" + id;
  }

  updateSensorUrl(id: string): string {
    return this.baseUrl + "config/sensor/" + id;
  }

  getAdminSensorsUrl(): string {
    return this.baseUrl + "config/sensors";
  }

  getSensorsUrl(): string {
    return this.baseUrl + "sensors";
  }

  addSensorsUrl(): string {
    return this.baseUrl + "config/sensors/add";
  }

  constructor() { }

  private baseUrl: string = environment.ApiBaseUrl;
  //private baseUrl: string = "https://localhost:32768/";

  getAuthUrl(): string {
    return this.baseUrl + "admin/login";
  }


  getCheckAuthUrl(): string {
    return this.baseUrl + "admin/check";
  }

  getCheckCorsUrl(): string {
    return this.baseUrl + "admin/cors";
  }
}
