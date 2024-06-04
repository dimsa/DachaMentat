import { Injectable } from '@angular/core';
import { IHttpClient } from "../common/IHttpClient";
import { WebClientService } from './web-client.service';
import { ApiSchemeService } from './api-scheme.service';
import { AuthDataDto } from '../dto/AuthDataDto';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  checkCors() {
    let getReq = this.httpClient.get(this.apiScheme.getCheckCorsUrl());
  }

  private httpClient: IHttpClient;

  private _isAuth: boolean = false;

  constructor(private apiScheme: ApiSchemeService, angHttpClient: WebClientService, private logger: LoggerService) {
    this.httpClient = angHttpClient as IHttpClient;
    //alert(this.httpClient);
  }

  public checkToken() {
    let getReq = this.httpClient.get(this.apiScheme.getCheckAuthUrl());
  }

  public get isAuth() {
    if (!this._isAuth) {
      let auth = this.httpClient.checkAuth();

      if (auth) {
        this._isAuth = true;
      }
    }

    return this._isAuth;
  }

  public async Auth(login: string, password: string) {

    //let getReq = this.httpClient.get(this.apiScheme.getCheckCorsUrl());

    let request = {} as any;
    request.UserName = login;
    request.Password = password;

    let auth = new AuthDataDto(login, password);

    let cont = "{\"userName\": \"string\",\"password\": \"string\"} ";
    let authResult = this.httpClient.post(this.apiScheme.getAuthUrl(), auth);
    await authResult
      .then((res: any) => {
        if (res.responseStatus.isSuccess) {
          this.httpClient.setAuth(res.token);
          this.logger.info(res);
          this._isAuth = true;
        } else {
          throw Error(res.responseStatus.message);
        }
      })
      .catch((error: any) => {
        console.error('Promise rejected with error: ' + error);
        throw Error('Promise rejected with error: ' + error);
    });
  }


  public logout() : void {
    this.httpClient.resetAuth();
    this._isAuth = false;
  }
}
