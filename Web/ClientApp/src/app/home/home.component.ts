import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Kudo, KudosService } from '../services/kudos.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  allKudos: Kudo[] = [];

  userId: string = '';
  userName: string = '';

  constructor(private jwtHelper: JwtHelperService, private kudosService: KudosService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    const token: any = localStorage.getItem('token');

    const decodedToken = this.jwtHelper.decodeToken(token);

    this.userId = decodedToken.id;
    this.userName = decodedToken.name;

    this.kudosService.getAllKudos().subscribe({
      next: kudos => {
        this.allKudos = kudos.reverse();
      },
      error: error => {
        console.log(error);
      }
    });
  }
}
