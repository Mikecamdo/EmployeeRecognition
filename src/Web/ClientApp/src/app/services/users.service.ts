import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, isDevMode } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginCredential, LoginResponse, SignupResponse, User, UserDto } from '../interfaces/user';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  apiRoot: string;
  headers: HttpHeaders = new HttpHeaders;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string,
  private tokenService: TokenService) {
    if (isDevMode()) {
      this.apiRoot = 'https://localhost:7140/users';
    } else {
      this.apiRoot = baseUrl + 'users';
    }
    
    this.tokenService.token$.subscribe((token: any) => {
      this.headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
    });
  }

  addUser(newUser: UserDto): Observable<SignupResponse> {
    return this.httpClient.post<SignupResponse>(this.apiRoot, newUser);
  }

  getUserBySignIn(loginCredential: LoginCredential): Observable<LoginResponse> {
    return this.httpClient.get<LoginResponse>(`${this.apiRoot}/login`, { params: {name: loginCredential.name, password: loginCredential.password }});
  }

  getUserByName(name: string): Observable<User> {
    return this.httpClient.get<User>(this.apiRoot + '/name', {params: {name: name}, headers: this.headers});
  }

  getAllUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(this.apiRoot, {headers: this.headers });
  }

  updateUser(currentUserId: string, currentUser: UserDto): Observable<SignupResponse> {
    let route: string = this.apiRoot + '/' + currentUserId;
    return this.httpClient.put<SignupResponse>(route, currentUser, {headers: this.headers });
  }

  deleteUser(userId: string): Observable<Object> {
    let route: string = this.apiRoot + '/' + userId;
    return this.httpClient.delete(route, {headers: this.headers });
  }
}