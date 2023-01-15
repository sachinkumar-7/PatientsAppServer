import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {PatientRoutingModule} from './patient-routing.module';
import {NgxPaginationModule} from "ngx-pagination";
import {PatientHomeComponent} from './patient-home/patient-home.component';
import {BookappointmentComponent} from "./book-appointment/book-ppointment.component";
import {FormsModule} from "@angular/forms";
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {JwtInterceptor} from "../helpers/interceptors/jwtInterceptor";
import {AgGridModule} from 'ag-grid-angular';
import {SearchDoctorComponent} from './search-doctor/search-doctor.component';


@NgModule({
  declarations: [
    PatientHomeComponent,
    BookappointmentComponent,
    SearchDoctorComponent,
  ],
  imports: [
    CommonModule,
    PatientRoutingModule,
    NgxPaginationModule,
    FormsModule,
    AgGridModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ]
})
export class PatientModule { }
