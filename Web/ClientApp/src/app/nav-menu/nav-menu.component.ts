import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TokenService } from '../services/token.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;

  userName: string = '';
  userAvatar: string = '';

  constructor(private router: Router, private jwtHelper: JwtHelperService, private tokenService: TokenService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    this.tokenService.token$.subscribe((token: any) => {
      const decodedToken = this.jwtHelper.decodeToken(token);
  
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
}
