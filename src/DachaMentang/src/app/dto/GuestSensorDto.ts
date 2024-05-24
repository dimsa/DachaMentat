import { CoordinatesDto } from "./CoordinatesDto";

export class GuestSensorDto {

  public id: string | undefined;
  public name: string | undefined;
  public coordinates: CoordinatesDto | undefined;
  public unitOfMeasure: string | undefined;
  public lastIndication: string | undefined;
  public lastIndicationTimeStamp: string;

  constructor(id: string, name: string, coordinates: CoordinatesDto, unitOfMeasure: string, lastIndication: string, lastIndicationTimeStamp: string) {
    this.id = id;
    this.name = name;
    this.coordinates = coordinates;
    this.unitOfMeasure = unitOfMeasure;
    this.lastIndication = lastIndication;
    this.lastIndicationTimeStamp = lastIndicationTimeStamp;
  }
}
