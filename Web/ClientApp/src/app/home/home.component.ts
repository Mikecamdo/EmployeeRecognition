import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  userId: string = '';
  userName: string = '';

  constructor(private jwtHelper: JwtHelperService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    const token: any = localStorage.getItem('token');

    const decodedToken = this.jwtHelper.decodeToken(token);

    this.userId = decodedToken.id;
    this.userName = decodedToken.name;
  }
}
