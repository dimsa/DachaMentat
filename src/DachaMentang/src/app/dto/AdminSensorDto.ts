import { TypedEvent } from "../misc/TypedEvent";
import { CoordinatesDto } from "./CoordinatesDto";

export class AdminSensorDto {

  public id: string;
  public name: string;
  public privateKey: string;

  public unitOfMeasure: string;

  public coordinates: CoordinatesDto;

  public onUpdate = new TypedEvent<AdminSensorDto>;
  
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
