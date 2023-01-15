import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {PharmacyRoutingModule} from './pharmacy-routing.module';
import {PharmacyHomeComponent} from './pharmacy-home/pharmacy-home.component';
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    PharmacyHomeComponent
  ],
  imports: [
    CommonModule,
    PharmacyRoutingModule,
    FormsModule
  ]
})
export class PharmacyModule {
}
