import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  apiRoot: string;
  headers: HttpHeaders;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //this.apiRoot = baseUrl + 'comments';
    this.apiRoot = 'https://localhost:7140' + '/comments';
    const token: any = localStorage.getItem('token');
    this.headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  }

  addComment(newComment: CommentDto): Observable<Comment> {
    return this.httpClient.post<Comment>(this.apiRoot, newComment, {headers: this.headers });
  }

  getAllComments(): Observable<Comment[]> {
    return this.httpClient.get<Comment[]>(this.apiRoot, {headers: this.headers });
  }

}

export interface CommentDto {
  kudoId: number;
  senderId: string;
  message: string;
}

export interface Comment {
  kudoId: number;
  senderId: string;
  senderName: string;
  senderAvatarUrl: string;
  message: string;
}