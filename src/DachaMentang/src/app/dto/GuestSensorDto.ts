import { CoordinatesDto } from "./CoordinatesDto";

export class GuestSensorDto {

  public id: string;
  public name: string;
  public coordinates: CoordinatesDto;
  public unitOfMeasure: string;
  public lastIndication: string;
  public lastIndicationTimeStamp: string;

  constructor(id: string, name: string, coordinates: CoordinatesDto, unitOfMeasure: string, lastIndication: string, lastIndicationTimeStamp: string) {
    this.id = id;
    this.name = name;
    this.coordinates = new CoordinatesDto()
    this.coordinates.latitude = coordinates.latitude;
    this.coordinates.longitude = coordinates.longitude;
    this.unitOfMeasure = unitOfMeasure;
    this.lastIndication = lastIndication;
    this.lastIndicationTimeStamp = lastIndicationTimeStamp;
  }
}
