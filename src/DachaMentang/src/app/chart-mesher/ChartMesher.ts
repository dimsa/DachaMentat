import { IndicationDto } from "../dto/IndicationDto";

export class ChartMesher {

  private isProcessed = false;

  private factTimeStamps: Date[] = [];

  private factIndications: number[] = [];

  private meshTimeStamps: string[] = [];

  private meshIndications: number[] = [];


  constructor(private indications: IndicationDto[]) {

    for (var i = 0; i < indications.length; i++) {
      let indication = +indications[i].value;
      let textTimeStamp = indications[i].timeStamp;
      let tmp = textTimeStamp.split(" ");

      let date = tmp[0].split(".");
      let time = tmp[1].split(":");


      let timeStamp = new Date();
      timeStamp.setUTCFullYear(+date[0]);
      timeStamp.setUTCMonth(+date[1] - 1);
      timeStamp.setUTCDate(+date[2]);

      timeStamp.setUTCHours(+time[0]);
      timeStamp.setUTCMinutes(+time[1]);
      timeStamp.setUTCSeconds(+time[2]);
      //date.
      this.factTimeStamps.push(timeStamp);
      this.factIndications.push(indication);
    }

    this.factIndications.reverse();
    this.factTimeStamps.reverse();
  }


  getSeries(): number[] {
    if (!this.isProcessed) {
      this.processSeries();
    }
    return this.meshIndications;
  }

  getTimeStamps(): string[] {
    if (!this.isProcessed) {
      this.processSeries();
    }
    return this.meshTimeStamps;
  }

  processSeries() {
    const dayCount = 15;
    const indicationPerDay = 12;
    const chartLength = dayCount * indicationPerDay;

    const twoHours = 1000 * 60 * 60 * 2;

    let currentTime = Date.now();
    var dateTime = new Date(+currentTime);
    this.meshTimeStamps = [];
    let localTimeStamps = [];
    for (var i = 0; i < chartLength; i++) {
      this.meshTimeStamps.push(dateTime.toLocaleDateString());
      localTimeStamps.push(dateTime);
      dateTime = new Date(+dateTime - twoHours);
    }

    this.meshIndications = [];
    for (var i = 0; i < chartLength; i++) {
      let value = this.interpolateValue(localTimeStamps[i]);
      this.meshIndications.push(value);
    }

   // this.meshTimeStamps.reverse();
    this.meshIndications.reverse();

    for (var i = 0; i < this.meshTimeStamps.length; i++) {
      if (i % 6 == 0 || i == this.meshTimeStamps.length - 1) {
        continue;
      }

      this.meshTimeStamps[i] = "";
    }

    this.isProcessed = true;
  }

  interpolateValue(period: Date) {
    //throw new Error("Method not implemented.");
    //let lesserDateInd = this.findFirstDateLess(period)
//      this.findInterpolationIndex(period);

    //let greaterDateInd = this.findFirstDateGreater(period);

    let left = this.findLeftIndex(period);
    let right = this.findRightIndex(period);

    /*if (lesserDateInd == -1) {
      lesserDateInd = 0;
      greaterDateInd = 0;
    } else
      if (lesserDateInd == this.factTimeStamps.length) {
        lesserDateInd = this.factTimeStamps.length - 1;
        greaterDateInd = this.factTimeStamps.length - 1;
      } else {
        greaterDateInd = lesserDateInd + 1;
      }*/


    //let greaterDateInd = Math.min(lesserDateInd + 1, this.factTimeStamps.length - 1) //this.findFirstDateGreater(period);

    let leftInd = this.factIndications[left];
    let rightInd = this.factIndications[right];

    let leftDate = this.factTimeStamps[left];
    let rightDate = this.factTimeStamps[right];

    if (left == 0 && right == this.factIndications.length - 1) {

      if (period < this.factTimeStamps[0]) {
        return leftInd;
      }

      if (period > this.factTimeStamps[this.factTimeStamps.length]) {
        return rightInd;
      }
    }

    let dx = (rightInd - leftInd);
    let dy = (+rightDate) - (+leftDate);
    let dCur = (+period) - (+leftDate);



    return leftInd + (dx / dy) * dCur;
  }

  findLeftIndex(period: Date): number {

      for (var i = this.factTimeStamps.length - 1; i > 0; i--) {
      if (period >= this.factTimeStamps[i]) {
        return i;
      }
    }


    return 0;
  }

  findRightIndex(period: Date): number {
    for (var i = this.factTimeStamps.length -1; i > 0 ; i--) {

      if (period <= this.factTimeStamps[i]) {
        return i;
      }
    }

    return this.factTimeStamps.length - 1;
  }

  findInterpolationIndex(period: Date): number {
    for (var i = 0; i < this.factTimeStamps.length; i++) {
      if (period >= this.factTimeStamps[i] && period <= this.factTimeStamps[i]) {
        return i;
      }
    }

    if (period <= this.factTimeStamps[0]) {
      return -1;
    }

    if (period >= this.factTimeStamps[this.factTimeStamps.length - 1]) {
      return this.factTimeStamps.length;
    }

    return -1
  }


}
