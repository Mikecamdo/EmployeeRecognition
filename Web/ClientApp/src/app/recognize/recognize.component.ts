import { Component, OnInit } from '@angular/core';
import { User, UsersService } from '../services/users.service';
import { Observable, OperatorFunction, debounceTime, distinctUntilChanged, map } from 'rxjs';

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

  constructor(private usersService: UsersService) { }

  ngOnInit(): void {
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

  autofillNames: OperatorFunction<string, readonly string[]> = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term) =>
        term.length < 2 ? [] : this.allUsers.map(user => user.name).filter((name) => name.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10),
      ),
    );
}
