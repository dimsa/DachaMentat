export class GuestSensorDto {

  public id: string | undefined;
  public name: string | undefined;
  public coordinates: string | undefined;
  public unitOfMeasure: string | undefined;
  public lastIndication: string | undefined;

  constructor(id: string, name: string, cordinates: string, unitOfMeasure: string, lastIndication: string) {
    this.id = id;
    this.name = name;
    this.coordinates = cordinates;
    this.unitOfMeasure = unitOfMeasure;
    this.lastIndication = lastIndication;
  }

}
