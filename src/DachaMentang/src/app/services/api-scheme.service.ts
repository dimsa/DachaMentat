import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiSchemeService {

  constructor() { }

  private baseUrl: string = "https://localhost:32768/";

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
