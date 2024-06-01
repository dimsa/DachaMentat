import { CoordinatesDto } from "./CoordinatesDto";
import { IndicationDto } from "./IndicationDto";

export class SensorMetaDto {

  public id: string;
  public name: string;
  public coordinates: CoordinatesDto;
  public unitOfMeasure: string;
  public indications: Array<IndicationDto>;

  constructor(id: string, name: string, coordinates: CoordinatesDto, unitOfMeasure: string, indications: IndicationDto[]) {
    this.id = id;
    this.name = name;
    this.coordinates = new CoordinatesDto()
    this.coordinates.latitude = coordinates.latitude;
    this.coordinates.longitude = coordinates.longitude;
    this.unitOfMeasure = unitOfMeasure;
    this.indications = indications;
  }

}
