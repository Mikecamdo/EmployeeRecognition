import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment, CommentDto } from '../interfaces/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  apiRoot: string;
  headers: HttpHeaders;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.apiRoot = baseUrl + 'comments';
    //this.apiRoot = 'https://localhost:7140' + '/comments';
    const token: any = localStorage.getItem('token');
    this.headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  }

  addComment(newComment: CommentDto): Observable<Comment> {
    return this.httpClient.post<Comment>(this.apiRoot, newComment, {headers: this.headers });
  }

  getAllComments(): Observable<Comment[]> {
    return this.httpClient.get<Comment[]>(this.apiRoot, {headers: this.headers });
  }

  deleteComment(commentId: number): Observable<Object> {
    let route = this.apiRoot + '/' + commentId;
    return this.httpClient.delete(route, {headers: this.headers });
  }

  updateComment(commentId: number, commentInfo: CommentDto): Observable<Comment> {
    let route = this.apiRoot + '/' + commentId;
    return this.httpClient.put<Comment>(route, commentInfo, {headers: this.headers });
  }

}