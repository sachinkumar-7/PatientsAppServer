import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from './home/home.component';
import {RoleGuard} from "../helpers/roleGuard";

const routes: Routes = [
  {
    path: 'doctor/home',
    component: HomeComponent,
    canActivate: [RoleGuard],
    data: {expectedRole: 'Doctor'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoctorRoutingModule { }
