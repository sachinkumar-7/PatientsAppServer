import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PharmacyHomeComponent} from "./pharmacy-home/pharmacy-home.component";

const routes: Routes = [
  {path: 'pharmacy', component: PharmacyHomeComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PharmacyRoutingModule {
}
