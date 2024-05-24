import { CoordinatesDto } from "./CoordinatesDto";

export class AdminSensorDto {

  public id: string;
  public name: string;
  public privateKey: string;
  /*public get coordinates(): CoordinatesDto {
    return this._coordinates as CoordinatesDto;
  }

  public set coordinates(value: object) {
    this._coordinates = value as CoordinatesDto;
  }*/

  public unitOfMeasure: string;

  //private _coordinates: CoordinatesDto;
  public coordinates: CoordinatesDto;
  

  constructor(id: string, privateKey: string, name: string, coordinates: CoordinatesDto, unitOfMeasure: string) {
    this.id = id;
    this.privateKey = privateKey;
    this.name = name;
    this.coordinates = new CoordinatesDto()
    this.coordinates.latitude = coordinates.latitude;
    this.coordinates.longitude = coordinates.longitude;
    
    this.unitOfMeasure = unitOfMeasure;
  }

}
