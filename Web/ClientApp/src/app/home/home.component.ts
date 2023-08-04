import { Component, HostListener, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { KudosService } from '../services/kudos.service';
import { CommentsService } from '../services/comments.service';
import { catchError, mergeMap, of, tap } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Kudo } from '../interfaces/kudo';
import { Comment, CommentDto } from '../interfaces/comment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  everythingLoaded: boolean = false;
  criticalError: boolean = false;

  allKudos: Kudo[] = [];
  allComments: Comment[] = [];

  commentMessage: string[] = [];
  hasComments: boolean[] = [];
  showComments: boolean[] = [];
  editingComment: boolean[] = [];

  userId: string = '';
  userName: string = '';
  userAvatar: string = '';

  constructor(private jwtHelper: JwtHelperService, private kudosService: KudosService,
    private commentsService: CommentsService, private router: Router, private toastr: ToastrService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    const token: any = localStorage.getItem('token');

    const decodedToken = this.jwtHelper.decodeToken(token);

    this.userId = decodedToken.id;
    this.userName = decodedToken.name;
    this.userAvatar = decodedToken.avatarUrl;

    this.kudosService.getAllKudos().pipe(
      tap(kudos => {
        this.allKudos = kudos.reverse();
        this.commentMessage = new Array(kudos.length).fill("");
      }),
      mergeMap(() => this.commentsService.getAllComments()),
      tap(comments => {
        this.allComments = comments;
        let kudosWithComments = comments.map(comment => comment.kudoId);
        this.allKudos.forEach((kudo, index) => {
          this.hasComments[index] = kudosWithComments.includes(kudo.id);
        });
        this.everythingLoaded = true;
        this.editingComment = new Array(comments.length).fill(false);
      }),
      catchError(error => {
        this.criticalError = true;
        console.log(error);
        return of([]);
      })
    ).subscribe();
  }

  activateButton(iteration: number): boolean {
    return !this.commentMessage[iteration];
  }

  activateComments(iteration: number): void {
    this.showComments[iteration] = !this.showComments[iteration];
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
        this.hasComments[iteration] = true;
        this.allComments.push(comment);
      },
      error: error => {
        this.toastr.error(error.error, "Error while adding comment");
      }
    });
  }

  isXsViewport(): boolean {
    return window.innerWidth < 768;
  }

  isMdViewport(): boolean {
    return window.innerWidth >= 768;
  }

  deleteKudo(kudoId: number, kudoIndex: number): void {
    this.kudosService.deleteKudo(kudoId).subscribe({
      next: x => {
        this.allKudos = this.allKudos.filter(kudo => kudo.id !== kudoId);
        this.hasComments.splice(kudoIndex, 1);
        this.showComments.splice(kudoIndex, 1);
        this.allComments = this.allComments.filter(comment => comment.kudoId !== kudoId);
      },
      error: error => {
        this.toastr.error(error.error, "Error while deleting kudo");
      }
    });
  }

  deleteComment(commentId: number, kudoId: number, kudoIndex: number): void {
    this.commentsService.deleteComment(commentId).subscribe({
      next: x => {
        this.allComments = this.allComments.filter(comment => comment.id !== commentId);
        let kudosWithComments = this.allComments.map(comment => comment.kudoId);
        this.hasComments[kudoIndex] = kudosWithComments.includes(kudoId);
        if (!kudosWithComments.includes(kudoId)) {
          this.showComments[kudoIndex] = false;
        }
      },
      error: error => {
        this.toastr.error(error.error, "Error while deleting comment");
      }
    });
  }

  updateComment(iteration: number, commentId: number, kudoId: number, updatedMessage: string): void {
    let updatedComment: CommentDto = {
      kudoId: kudoId,
      senderId: this.userId,
      message: updatedMessage
    };

    this.commentsService.updateComment(commentId, updatedComment).subscribe({
      next: (comment: Comment) => {
        this.allComments[iteration] = comment;
        this.editingComment[iteration] = false;
      },
      error: error => {
        this.toastr.error(error.error, "Error while updating comment");
      }
    });
  }

  editComment(iteration: number, commentId: number, kudoId: number, updatedMessage: string): void {
    if (this.editingComment[iteration]) {
      this.updateComment(iteration, commentId, kudoId, updatedMessage);
    }
    this.editingComment[iteration] = !this.editingComment[iteration];
  }

  activateUpdateButton(message: string): boolean {
    return !message;
  }

  @HostListener('window:resize', ['$event'])
  onWindowResize(event: Event): void {
    this.isXsViewport();
    this.isMdViewport();

    this.exampleImageLarge();
    this.exampleImageMedium();
    this.exampleImageSmall();
    this.exampleImageNone();
  }

  exampleImageLarge(): boolean {
    return window.innerWidth >= 1400;
  }

  exampleImageMedium(): boolean {
    return window.innerWidth >= 1200 && window.innerWidth < 1400;
  }

  exampleImageSmall(): boolean {
    return window.innerWidth >= 992 && window.innerWidth < 1200;
  }

  exampleImageSmallest(): boolean {
    return window.innerWidth >= 768 && window.innerWidth < 992;
  }

  exampleImageNone(): boolean {
    return window.innerWidth < 768;
  }
}
