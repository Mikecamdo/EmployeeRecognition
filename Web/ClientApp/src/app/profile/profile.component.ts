import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserDto, UsersService } from '../services/users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  characterSet: string = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

  changesSaved: boolean = false; //FIXME change this to a popup

  userName: string = '';
  userAvatar: string = '';
  userId: string = '';

  oldPassword: string = '';
  newPassword: string = '';
  confirmNewPassword: string = '';
  
  constructor(private jwtHelper: JwtHelperService, private usersService: UsersService) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    const token: any = localStorage.getItem('token');

    const decodedToken = this.jwtHelper.decodeToken(token);

    this.userName = decodedToken.name;
    this.userAvatar = decodedToken.avatarUrl;
    this.userId = decodedToken.id;
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
    this.changesSaved = false;
    let userInfo: UserDto = {
      name: this.userName,
      password: 'test', //FIXME need to fix this
      avatarUrl: this.userAvatar
    };

    this.usersService.updateUser(this.userId, userInfo).subscribe({
      next: updatedUser => {
        console.log("Success!");
        this.changesSaved = true;
      },
      error: error => {
        console.log(error);
      }
    })
  }

}
