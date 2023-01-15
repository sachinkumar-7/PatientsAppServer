import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {BookappointmentComponent} from "./book-appointment/book-ppointment.component";
import {PatientHomeComponent} from "./patient-home/patient-home.component";
import {SearchDoctorComponent} from "./search-doctor/search-doctor.component";
import {RoleGuard} from "../helpers/roleGuard";

const routes: Routes = [
  {
    path: 'User/home', component: PatientHomeComponent, canActivate: [RoleGuard],
    data: {expectedRole: 'User'}
  },
  {
    path: 'User/home/book', component: BookappointmentComponent, canActivate: [RoleGuard],
    data: {expectedRole: 'User'}
  },
  {
    path: 'User/home/search', component: SearchDoctorComponent, canActivate: [RoleGuard],
    data: {expectedRole: 'User'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PatientRoutingModule { }
