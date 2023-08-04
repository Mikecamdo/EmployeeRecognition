import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UsersService } from '../services/users.service';
import { TokenService } from '../services/token.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginCredential, UserDto } from '../interfaces/user';

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
  editingPassword: boolean = false;

  characterSet: string = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

  currentUserName: string = '';
  userName: string = '';
  userAvatar: string = '';
  userId: string = '';
  userBio: string = '';

  oldPassword: string = '';
  newPassword: string = '';
  confirmNewPassword: string = '';
  
  constructor(private jwtHelper: JwtHelperService, private usersService: UsersService, 
    private tokenService: TokenService, private router: Router, private route: ActivatedRoute,
    private toastr: ToastrService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const name: any = params.get('name');
      this.userName = name;

      this.usersService.getUserByName(name).subscribe({
        next: user => {
          this.currentUserName = user.name;
          this.userAvatar = user.avatarUrl;
          this.userId = user.id;
          this.userBio = user.bio;

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
          this.toastr.error("Incorrect old password", "Error while updating password");
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
      avatarUrl: this.userAvatar,
      bio: this.userBio
    };

    this.usersService.updateUser(this.userId, userInfo).subscribe({
      next: response => { 
        this.tokenService.updateToken(response.token);
        this.editingProfile = false;
        this.editingPassword = false;
        this.oldPassword = '';
        this.newPassword = '';
      },
      error: error => {
        this.toastr.error(error.error, "Error while updating user");
      }
    });
  }

  editProfile(): void {
    this.editingProfile = true;
  }

  changePassword(): void {
    this.editingPassword = true;
  }

  deleteAccount(): void {
    this.usersService.deleteUser(this.userId).subscribe({
      next: response => {
        localStorage.removeItem("token");
        this.router.navigate(["/"]);
      },
      error: error => {
        this.toastr.error(error.error, "Error while deleting user");
      }
    });
  }

  invalidPassword(): boolean {
    if (this.confirmNewPassword !== '' && this.confirmNewPassword !== this.newPassword) {
      return true;
    }
    return false;
  }

  disableButton(button: number): boolean {
    if (button === 1) {
      return !this.userName || !this.userBio || !this.userAvatar;
    } else { //button === 2
      return !this.oldPassword || !this.newPassword || !this.confirmNewPassword || this.invalidPassword();
    }
  }
}
