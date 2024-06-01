export class IndicationDto {

  //public id: string;
  public value: string;
  public timeStamp: string;

  constructor(value: string, timeStamp: string) {
    //this.id = id;
    this.value = value;
    this.timeStamp = timeStamp;
    
  }
}
