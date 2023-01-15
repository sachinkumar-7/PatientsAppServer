import {Injectable} from '@angular/core';
import {CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {AuthServiceService} from "../auth/auth-service.service";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authenticationService: AuthServiceService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRole = route.data['expectedRole'];
    const user = localStorage.getItem('role');

    const helper = new JwtHelperService();
    const token: any = localStorage.getItem('token');
    const isExpired = helper.isTokenExpired(token);

    if (!user || user !== expectedRole || isExpired) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
