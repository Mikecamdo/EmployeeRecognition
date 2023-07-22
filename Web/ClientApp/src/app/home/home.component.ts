import { Component, HostListener, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Kudo, KudosService } from '../services/kudos.service';
import { Comment, CommentDto, CommentsService } from '../services/comments.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  allKudos: Kudo[] = [];

  allComments: Comment[] = [];
  commentMessage: string[] = [];

  userId: string = '';
  userName: string = '';
  userAvatar: string = '';

  constructor(private jwtHelper: JwtHelperService, private kudosService: KudosService,
    private commentsService: CommentsService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    const token: any = localStorage.getItem('token');

    const decodedToken = this.jwtHelper.decodeToken(token);

    this.userId = decodedToken.id;
    this.userName = decodedToken.name;
    this.userAvatar = decodedToken.avatarUrl;

    this.kudosService.getAllKudos().subscribe({
      next: kudos => {
        this.allKudos = kudos.reverse(); //FIXME I think I can reverse this in the backend database call
        this.commentMessage = new Array(kudos.length).fill("");
      },
      error: error => {
        console.log(error);
      }
    });

    this.commentsService.getAllComments().subscribe({
      next: comments => {
        this.allComments = comments;
      },
      error: error => {
        console.log(error);
      }
    });
  }

  activateButton(iteration: number): boolean {
    return !this.commentMessage[iteration];
  }

  addComment(id: number, iteration: number): void {
    let comment: CommentDto = {
      kudoId: id,
      senderId: this.userId,
      message: this.commentMessage[iteration]
    };

    this.commentsService.addComment(comment).subscribe({
      next: comment => {
        this.commentMessage[iteration] = '';
        this.allComments.push(comment); //FIXME make sure this works
      },
      error: error => {
        console.log(error);
      }
    });
  }

  isXsViewport(): boolean {
    return window.innerWidth < 768;
  }

  isMdViewport(): boolean {
    return window.innerWidth >= 768;
  }

  @HostListener('window:resize', ['$event'])
  onWindowResize(event: Event): void {
    this.isXsViewport();
    this.isMdViewport();
  }
}
