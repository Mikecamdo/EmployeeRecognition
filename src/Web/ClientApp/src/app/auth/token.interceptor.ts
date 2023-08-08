import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  
  constructor(private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');

    if (token) {
      const expirationDate = new Date(0);
      expirationDate.setUTCSeconds(this.getExpirationTime(token));

      if (expirationDate <= new Date()) {
        localStorage.removeItem("token");
        this.router.navigate(["/"]);
      }
    }

    return next.handle(request);
  }

  private getExpirationTime(token: string): number {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload.exp;
  }
}
