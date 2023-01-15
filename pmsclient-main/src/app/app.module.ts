import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import {AppComponent} from './app.component';
import {PatientModule} from "./patient/patient.module";
import {HttpClientModule} from "@angular/common/http";
import {LoginComponent} from "./auth/login/login.component";
import {AuthModule} from "./auth/auth.module";
import {AdminModule} from "./admin/admin.module";
import {DoctorModule} from "./doctor/doctor.module";
import {NgxPaginationModule} from "ngx-pagination";
import {FormsModule} from "@angular/forms";
import {PharmacyModule} from "./pharmacy/pharmacy.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PatientModule,
    AuthModule,
    HttpClientModule,
    AdminModule,
    DoctorModule,
    NgxPaginationModule,
    FormsModule,
    PharmacyModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
