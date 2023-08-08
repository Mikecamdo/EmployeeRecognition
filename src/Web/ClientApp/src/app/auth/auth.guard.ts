import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (localStorage.getItem("token")) { //if there is a JWT token, then the user is logged in
      if (state.url === '' || state.url ==='/') {
        this.router.navigate(['/home']);
        return false;
      } else {
        return true;
      }
    } else {
      if (state.url === '' || state.url ==='/') {
        return true;
      } else {
        this.router.navigate(['/']);
        return false;
      }
    }
  }
}
