import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KudosService {

  apiRoot: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //this.apiRoot = baseUrl + 'kudos';
    this.apiRoot = 'https://localhost:7140' + '/kudos';
  }

  addKudo(newKudo: KudoDto): Observable<Kudo> {
    return this.httpClient.post<Kudo>(this.apiRoot, newKudo);
  }

  getAllKudos(): Observable<Kudo[]> {
    return this.httpClient.get<Kudo[]>(this.apiRoot);
  }
  
}

export interface KudoDto {
  sender: string;
  senderId: string;
  senderAvatar: string;
  receiver: string;
  receiverId: string;
  receiverAvatar: string;
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

export interface Kudo {
  id: number;
  sender: string;
  senderId: string;
  senderAvatar: string;
  receiver: string;
  receiverId: string;
  receiverAvatar: string;
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