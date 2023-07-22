import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KudosService {

  apiRoot: string;
  headers: HttpHeaders;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //this.apiRoot = baseUrl + 'kudos';
    this.apiRoot = 'https://localhost:7140' + '/kudos';
    const token: any = localStorage.getItem('token');
    this.headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  }

  addKudo(newKudo: KudoDto): Observable<Kudo> {
    return this.httpClient.post<Kudo>(this.apiRoot, newKudo, {headers: this.headers });
  }

  getAllKudos(): Observable<Kudo[]> {
    return this.httpClient.get<Kudo[]>(this.apiRoot, {headers: this.headers });
  }
  
}

export interface KudoDto {
  senderId: string;
  receiverId: string;
  title: string;
  message: string;
  teamPlayer: boolean;
  oneOfAKind: boolean;
  creative: boolean;
  highEnergy: boolean;
  awesome: boolean;
  achiever: boolean;
  sweetness: boolean;
}

export interface Kudo {
  id: number;
  senderName: string;
  senderId: string;
  senderAvatarUrl: string;
  receiverName: string;
  receiverId: string;
  receiverAvatarUrl: string;
  title: string;
  message: string;
  teamPlayer: boolean;
  oneOfAKind: boolean;
  creative: boolean;
  highEnergy: boolean;
  awesome: boolean;
  achiever: boolean;
  sweetness: boolean;
  theDate: string;
}