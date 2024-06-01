import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AdminComponent } from './admin/admin.component';
import { ConfigComponent } from './config/config.component';
import { ChartsComponent } from './charts/charts.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { MenuComponent } from './menu/menu.component';
import { AppRoutingModule } from './app-routing.module';
import { AccountComponent } from './account/account.component';
import { FormsModule } from '@angular/forms';
import { AdminSensorMetaItemComponent } from './admin-sensor-meta-item/admin-sensor-meta-item.component';
import { SensorsComponent } from './sensors/sensors.component';
import { GuestSensorMetaItemComponent } from './guest-sensor-meta-item/guest-sensor-meta-item.component';


import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import * as echarts from 'echarts/core';
import { BarChart, LineChart } from 'echarts/charts';
import {
  TitleComponent,
  TooltipComponent,
  GridComponent,
  DatasetComponent,
  TransformComponent
} from 'echarts/components';
import { LabelLayout, UniversalTransition } from 'echarts/features';
import { CanvasRenderer } from 'echarts/renderers';
import 'echarts/theme/macarons.js';
import { SensorComponent } from './sensor/sensor.component';

echarts.use([
  BarChart,
  LineChart,
  TitleComponent,
  TooltipComponent,
  GridComponent,
  DatasetComponent,
  TransformComponent,
  LabelLayout,
  UniversalTransition,
  CanvasRenderer
]);

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    ConfigComponent,
    ChartsComponent,
    PageNotFoundComponent,
    MenuComponent,
    AccountComponent,
    AdminSensorMetaItemComponent,
    SensorsComponent,
    GuestSensorMetaItemComponent,
    SensorComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule, FormsModule,
    NgxEchartsDirective
  ],
  providers: [provideEchartsCore({ echarts })],
  bootstrap: [AppComponent]
})
export class AppModule { }
