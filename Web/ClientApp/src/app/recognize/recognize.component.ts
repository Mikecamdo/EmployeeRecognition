import { Component, OnInit } from '@angular/core';
import { User, UsersService } from '../services/users.service';
import { Observable, OperatorFunction, debounceTime, distinctUntilChanged, map } from 'rxjs';
import { KudoDto, KudosService } from '../services/kudos.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-recognize',
  templateUrl: './recognize.component.html',
  styleUrls: ['./recognize.component.css']
})
export class RecognizeComponent implements OnInit {
  allUsers: User[] = [];
  receiver: string = '';
  title: string = '';
  message: string = '';

  //The various tags that can be given
  teamPlayer: boolean = false;
  oneOfAKind: boolean = false;
  creative: boolean = false;
  highEnergy: boolean = false;
  awesome: boolean = false;
  achiever: boolean = false;
  sweetness: boolean = false;

  senderId: string = '';

  constructor(private usersService: UsersService, 
              private kudosService: KudosService,
              private router: Router,
              private jwtHelper: JwtHelperService) 
  {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    const token: any = localStorage.getItem('token');
    const decodedToken = this.jwtHelper.decodeToken(token);
    this.senderId = decodedToken.id;

    this.usersService.getAllUsers().subscribe({
      next: users => {
        this.allUsers = users;
      },
      error: error => {
        console.log(error);
      }
    });
  }

  changeStatus(event: any, tag: number) {
    switch(tag) {
      case 1:
        this.teamPlayer = event.target.checked;
        break;

      case 2:
        this.oneOfAKind = event.target.checked;
        break;

      case 3:
        this.creative = event.target.checked;
        break;

      case 4:
        this.highEnergy = event.target.checked;
        break;

      case 5:
        this.awesome = event.target.checked;
        break;

      case 6:
        this.achiever = event.target.checked;
        break;

      case 7:
        this.sweetness = event.target.checked;
        break;
    }
  }

  activateButton(): boolean {
    return !!this.receiver && !!this.title && !!this.message && !! this.allUsers.map(user => user.name).find(x => x === this.receiver);
  }

  autofillNames: OperatorFunction<string, readonly string[]> = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term) =>
        term.length < 2 ? [] : this.allUsers.map(user => user.name).filter((name) => name.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10),
      ),
    );

  addKudo(): void {
    let receiverId: any = this.allUsers.find(user => user.name === this.receiver)?.id;
    let newKudo: KudoDto = {
      senderId: this.senderId,
      receiverId: receiverId,
      title: this.title,
      message: this.message,
      teamPlayer: this.teamPlayer,
      oneOfAKind: this.oneOfAKind,
      creative: this.creative,
      highEnergy: this.highEnergy,
      awesome: this.awesome,
      achiever: this.achiever,
      sweetness: this.sweetness
    };

    this.kudosService.addKudo(newKudo).subscribe({
      next: addedKudo => {
        console.log("Added:");
        console.log(addedKudo);
      },
      error: error => {
        console.log(error);
      }
    })
  }
}
