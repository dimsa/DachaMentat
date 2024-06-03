import { CoordinatesDto } from "../dto/CoordinatesDto";

export class ChartData {
  public id: string;
  public name: string;
  public coordinates: CoordinatesDto;
  public unitOfMeasure: string;
  public timeStamps: string[] | undefined;
  public series: number[] | undefined;
  public loaded: boolean = false;

  constructor(id: string, name: string, coordinates: CoordinatesDto, unitOfMeasure: string) {
    this.id = id;
    this.name = name;
    this.coordinates = new CoordinatesDto()
    this.coordinates.latitude = coordinates.latitude;
    this.coordinates.longitude = coordinates.longitude;
    this.unitOfMeasure = unitOfMeasure;
  }
}
