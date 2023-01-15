import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AdminHomeComponent} from "./admin-home/admin-home.component";
import {RoleGuard} from "../helpers/roleGuard";

const routes: Routes = [
  {
    path: 'admin/home', component: AdminHomeComponent, canActivate: [RoleGuard],
    data: {expectedRole: 'Admin'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
