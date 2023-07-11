import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  apiRoot: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //this.apiRoot = baseUrl + 'users';
    this.apiRoot = 'https://localhost:7140' + '/users';
  }

  addUser(newUser: UserDto): Observable<User> {
    return this.httpClient.post<User>(this.apiRoot, newUser);
  }

  getUserBySignIn(loginCredential: LoginCredential): Observable<User> { //FIXME need to figure out where to hash the password
    return this.httpClient.get<User>(`${this.apiRoot}/login`, { params: {name: loginCredential.name, password: loginCredential.password }}); 
  }

  getAllUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(this.apiRoot);
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