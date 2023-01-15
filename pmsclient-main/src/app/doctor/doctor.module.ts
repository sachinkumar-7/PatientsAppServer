import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {DoctorRoutingModule} from './doctor-routing.module';
import {HomeComponent} from './home/home.component';
import {NgxPaginationModule} from "ngx-pagination";
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    DoctorRoutingModule,
    NgxPaginationModule,
    FormsModule
  ]
})
export class DoctorModule { }
