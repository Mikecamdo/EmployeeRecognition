import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  apiRoot: string;
  headers: HttpHeaders;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //this.apiRoot = baseUrl + 'users';
    this.apiRoot = 'https://localhost:7140' + '/users';
    const token: any = localStorage.getItem('token');
    this.headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  }

  addUser(newUser: UserDto): Observable<SignupResponse> {
    return this.httpClient.post<SignupResponse>(this.apiRoot, newUser);
  }

  getUserBySignIn(loginCredential: LoginCredential): Observable<LoginResponse> { //FIXME need to figure out where to hash the password
    return this.httpClient.get<LoginResponse>(`${this.apiRoot}/login`, { params: {name: loginCredential.name, password: loginCredential.password }});
  }

  getAllUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(this.apiRoot, {headers: this.headers });
  }
}

export interface UserDto { //FIXME eventually want to move this to its own file
  name: string;
  password: string;
  avatarUrl: string;
}

export interface User {
  id: string;
  name: string;
  password: string;
  avatarUrl: string;
}

export interface LoginCredential {
  name: string;
  password: string;
}

export interface LoginResponse {
  isLoginSuccessful: boolean;
  errorMessage: string;
  token: string;
}

export interface SignupResponse {
  isSignupSuccessful: boolean;
  errorMessage: string;
  token: string;
}