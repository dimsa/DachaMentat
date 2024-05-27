export class CoordinatesDto {

  public toString = (): string => {
    return this.longitude + ", " + this.latitude;
  }


  public latitude: number = 0
  public longitude: number = 0

  constructor();
  constructor(value: string);
  constructor(value?: string) {
    if (value) {
      let splits = value.split(',');

      if (splits.length > 2) {
        throw new Error("Can not process coordinates");
      }

      let lat = +splits[0].trim();;
      let long = +splits[1].trim();;
      this.latitude = lat;
      this.longitude = long;
    }
  }
}
