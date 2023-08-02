import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TokenService } from '../services/token.service';
import { UserDataService } from '../services/user-data.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;

  userId: string = '';
  userName: string = '';
  userAvatar: string = '';

  constructor(private router: Router, private jwtHelper: JwtHelperService, 
  private tokenService: TokenService, private userDataService: UserDataService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    let currentToken: any = localStorage.getItem("token");
    this.tokenService.updateToken(currentToken);

    this.tokenService.token$.subscribe((token: any) => {
      const decodedToken = this.jwtHelper.decodeToken(token);
  
      this.userId = decodedToken.id;
      this.userName = decodedToken.name;
      this.userAvatar = decodedToken.avatarUrl + '&flip=true';
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(): void {
    localStorage.removeItem("token");
    this.router.navigate(["/"]);
  }

  viewProfile(): void {
    this.userDataService.setUserData({
      id: this.userId,
      name: this.userName,
      avatarUrl: this.userAvatar
    });

    this.router.navigate(['/profile', this.userName]);
  }
}
