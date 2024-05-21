export class AdminSensorDto {

  public id: string | undefined;
  public name: string | undefined;
  public privateKey: string | undefined;
  public coordinates: string | undefined;
  public unitOfMeasure: string | undefined;

  constructor(id: string, privateKey: string, name: string, cordinates: string, unitOfMeasure: string) {
    this.id = id;
    this.privateKey = privateKey;
    this.name = name;
    this.coordinates = cordinates;
    this.unitOfMeasure = unitOfMeasure;
  }

}
