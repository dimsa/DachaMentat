export class CoordinatesDto {

  public toString = (): string => {
    return this.longitude + ", " + this.latitude;
  }


  public latitude: number = 0
  public longitude: number = 0

  constructor() {
  }
}
