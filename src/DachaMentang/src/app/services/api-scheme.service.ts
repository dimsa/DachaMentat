import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiSchemeService {
  getSensorsUrl(): string {
    return this.baseUrl + "sensors";
  }

  addSensorsUrl(): string {
    return this.baseUrl + "sensors/add";
  }

  constructor() { }

  private baseUrl: string = "http://localhost:5219/";
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
