import { Component, Input } from '@angular/core';
import { GuestSensorDto } from '../dto/GuestSensorDto';

@Component({
  selector: '[app-guest-sensor-meta-item]',
  templateUrl: './guest-sensor-meta-item.component.html',
  styleUrls: ['./guest-sensor-meta-item.component.css']
})
export class GuestSensorMetaItemComponent {
  @Input() public data: GuestSensorDto = new GuestSensorDto("0", "0", "0", "0", "0", "0");
}
