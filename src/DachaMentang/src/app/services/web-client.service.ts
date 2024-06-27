import { HttpClient, HttpEvent, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { IHttpClient } from "../common/IHttpClient";
import { Observable, firstValueFrom } from 'rxjs';
import { ILogger } from '../common/ILogger';
import { Injectable } from '@angular/core';
import { LoggerService } from './logger.service';


@Injectable({
  providedIn: 'root'
})
export class WebClientService implements IHttpClient {

  private httpOptions: HttpHeaders;
  token: string;
  private tokenKey: string = 'MentangToken';


  async post<T>(url: string, body: any): Promise<T> {
    this._logger.log("Post Request Started", url, body);
 //   let opt = this.httpOptions;
    //let value = this._httpClient.post(url, body, { headers: opt });

    let value;

    if (this.token) {
      value = this._httpClient.post(url, body, {
        headers: {
          "Authorization": "Bearer " + this.token
        }
      });
    } else {
      value = this._httpClient.post(url, body);
    }
    
    return await firstValueFrom(value) as T;
  }

  async get<T>(url: string): Promise<T> {
    this._logger.log("Get Request Started", url);
   // let opt = this.httpOptions;

    let value;

    if (this.token) {
      value = this._httpClient.get(url, {
        headers: {
          "Authorization": "Bearer " + this.token
        }
      });
    } else {
      value = this._httpClient.get(url);
    }

    return await firstValueFrom(value) as T;
  }

  constructor(private _httpClient: HttpClient, private _logger: LoggerService) {
    this.token = "";
    this.httpOptions = new HttpHeaders();
    this._logger.log("AngularHttpClientWrapperCreated", _httpClient, _logger);
  }


    checkAuth(): boolean {
      if (!this.token) {
        let savedToken = localStorage.getItem(this.tokenKey);
        if (savedToken) {
          this.token = savedToken;
          return true;
        }
      }

      if (this.token) {
        return true;
      }

      return false;      
    }

  setAuth(token: string): void {
    this.token = token;
    this.httpOptions.set("Authorization", "Bearer " + token);
    localStorage.setItem(this.tokenKey, token);
  }

  resetAuth(): void {
    localStorage.removeItem(this.tokenKey);
    this.token = "";
    this.httpOptions.delete("Authorization");
  }

}
