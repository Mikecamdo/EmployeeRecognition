import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Kudo, KudoDto } from '../interfaces/kudo';

@Injectable({
  providedIn: 'root'
})
export class KudosService {

  apiRoot: string;
  headers: HttpHeaders;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.apiRoot = baseUrl + 'api/kudos';
    //this.apiRoot = 'https://localhost:7140' + '/kudos';
    const token: any = localStorage.getItem('token');
    this.headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  }

  addKudo(newKudo: KudoDto): Observable<Kudo> {
    return this.httpClient.post<Kudo>(this.apiRoot, newKudo, {headers: this.headers });
  }

  getAllKudos(): Observable<Kudo[]> {
    return this.httpClient.get<Kudo[]>(this.apiRoot, {headers: this.headers });
  }

  deleteKudo(kudoId: number): Observable<Object> {
    let route = this.apiRoot + '/' + kudoId;
    return this.httpClient.delete(route, {headers: this.headers });
  }
  
}