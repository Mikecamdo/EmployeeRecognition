import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, isDevMode } from '@angular/core';
import { Observable } from 'rxjs';
import { Kudo, KudoDto } from '../interfaces/kudo';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class KudosService {

  apiRoot: string;
  headers: HttpHeaders = new HttpHeaders;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string,
  private tokenService: TokenService) {
    if (isDevMode()) {
      this.apiRoot = 'https://localhost:7140/kudos';
    } else {
      this.apiRoot = baseUrl + 'kudos';
    }
    
    this.tokenService.token$.subscribe((token: any) => {
      this.headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
    });
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