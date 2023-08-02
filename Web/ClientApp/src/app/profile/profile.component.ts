import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginCredential, LoginResponse, UserDto, UsersService } from '../services/users.service';
import { TokenService } from '../services/token.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  everythingLoaded: boolean = false;
  criticalError: boolean = false;

  isCurrentUser: boolean = false;
  editingProfile: boolean = false;

  characterSet: string = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

  showMessage: boolean = false; //FIXME change this to a popup (toastr??)
  message: string = '';

  currentUserName: string = '';
  userName: string = '';
  userAvatar: string = '';
  userId: string = '';

  oldPassword: string = '';
  newPassword: string = '';
  confirmNewPassword: string = '';
  
  constructor(private jwtHelper: JwtHelperService, private usersService: UsersService, 
    private tokenService: TokenService, private router: Router, private route: ActivatedRoute) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const name: any = params.get('name');

      this.usersService.getUserByName(name).subscribe({
        next: user => {
          this.userName = user.name;
          this.currentUserName = user.name;
          this.userAvatar = user.avatarUrl;
          this.userId = user.id;

          const token: any = localStorage.getItem('token');
          const decodedToken = this.jwtHelper.decodeToken(token);

          if (decodedToken.id === user.id) {
            this.isCurrentUser = true;
          }
          this.everythingLoaded = true;
          this.criticalError = false;
        },
        error: error => {
          console.log(error);
          this.criticalError = true;
        }
      });
    });
  }

  generateNewAvatar(): void {
    let randomString = '';
    for (let i = 0; i < 5; i++) {
      const randomIndex = Math.floor(Math.random() * this.characterSet.length);
      randomString += this.characterSet[randomIndex];
    }

    let newAvatar: string = 'https://api.dicebear.com/6.x/miniavs/svg?seed=' + randomString;
    this.userAvatar = newAvatar;
  }

  saveChanges(): void {
    this.showMessage = false;

    if (this.oldPassword) {
      let loginAttempt: LoginCredential = {
        name: this.currentUserName,
        password: this.oldPassword
      }
      this.usersService.getUserBySignIn(loginAttempt).subscribe({
        next: response => {
          this.updateUser(true);
        },
        error: error => {
          console.log(error);
          this.message = "Incorrect old password, changes not saved";
          this.showMessage = true;
        }
      });
    } else {
      this.updateUser(false);
    }
  }

  updateUser(updatedPassword: boolean): void {
    let userInfo: UserDto = {
      name: this.userName,
      password: updatedPassword ? this.newPassword : null,
      avatarUrl: this.userAvatar
    };

    this.usersService.updateUser(this.userId, userInfo).subscribe({
      next: response => { 
        this.tokenService.updateToken(response.token);
        this.editingProfile = false;
      },
      error: error => {
        console.log(error);
        if (error.status == 400) {
          this.message = "Name already in use, please choose another name";
          this.showMessage = true;
        }
      }
    });
  }

  editProfile(): void {
    this.editingProfile = true;
  }

  deleteAccount(): void {
    this.usersService.deleteUser(this.userId).subscribe({
      next: response => {
        localStorage.removeItem("token");
        this.router.navigate(["/"]);
      },
      error: error => {
        console.log(error);
      }
    });
  }
}
