import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {AdminRoutingModule} from './admin-routing.module';
import {AgGridModule} from "ag-grid-angular";
import {FormsModule} from "@angular/forms";
import {AdminHomeComponent} from "./admin-home/admin-home.component";


@NgModule({
  declarations: [
    AdminHomeComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    AgGridModule,
    FormsModule
  ]
})
export class AdminModule { }
