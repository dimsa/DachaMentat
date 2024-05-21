import { Injectable } from '@angular/core';
import { ILogger } from '../common/ILogger';

@Injectable({
  providedIn: 'root'
})
export class LoggerService implements ILogger {

  log(message?: any, ...optionalParams: any[]): void {

   // if (environment.production) {
   //   return;
  //  }

    console.log(message, ...optionalParams);
  }

  info(message?: any, ...optionalParams: any[]): void {
  //  if (environment.production) {
  //    return;
 //   }

    console.info(message, ...optionalParams);
  }

  warn(message?: any, ...optionalParams: any[]): void {
    // Warn and Error are available in prod mode
    console.warn(message, ...optionalParams);
  }

  error(message?: any, ...optionalParams: any[]): void {
    // Warn and Error are available in prod mode
    console.error(message, ...optionalParams);
  }

  constructor() { }
}
