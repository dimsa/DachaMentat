import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ChartsComponent } from './charts/charts.component';
import { ConfigComponent } from './config/config.component';
import { AdminComponent } from './admin/admin.component';
import { SensorsComponent } from './sensors/sensors.component';

const appRoutes: Routes = [
  { path: 'auth', component: AdminComponent },
  { path: 'config', component: ConfigComponent },
  { path: 'charts', component: ChartsComponent },
  { path: 'chart/:id', component: ChartsComponent },
  { path: 'sensors', component: SensorsComponent },
  { path: 'sensor/:id', component: SensorsComponent },
  { path: '', redirectTo: "/charts", pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }  // #enddocregion wildcard
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
