import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private tokenSource = new BehaviorSubject(localStorage.getItem("token"));
  token$ = this.tokenSource.asObservable();

  getToken() : any {
    return this.tokenSource.getValue();
  }

  updateToken(newToken: string): void {
    this.tokenSource.next(newToken);
    localStorage.setItem("token", newToken);
  }
}
