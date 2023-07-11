import { Component } from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { LoginCredential, UserDto, UsersService } from '../services/users.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {
  signUpName: string = '';
  signUpPassword: string = '';
  signUpConfirmPassword: string = '';

  signInName: string = '';
  signInPassword: string = '';
  
  constructor(private modalService: NgbModal, private usersService: UsersService) { }
  
  open(content: any, label: string) {
    const modalRef = this.modalService.open(content, {ariaLabelledBy: label});
    
    modalRef.result.then((result) => {
      this.signUpName = '';
      this.signUpPassword = '';
      this.signUpConfirmPassword = ''; //FIXME I think these don't do anything
      this.signInName = '';
      this.signInPassword = '';
    }, (reason) => {
      this.signUpName = '';
      this.signUpPassword = '';
      this.signUpConfirmPassword = '';
      this.signInName = '';
      this.signInPassword = '';
    });
  }

  attemptSignUp(): void {
    let newUser: UserDto = {
      name: this.signUpName,
      password: this.signUpPassword,
      avatarUrl: "placeholder"
    }
    this.usersService.addUser(newUser).subscribe({
      next: response => {
        console.log("Successfully signed up!");
        console.log(response);
      },
      error: error => {
        console.log("Error while signing up");
        console.log(error);
      }
    });
  }

  attemptSignIn(): void {
    let loginAttempt: LoginCredential = {
      name: this.signInName,
      password: this.signInPassword
    }
    this.usersService.getUserBySignIn(loginAttempt).subscribe({
      next: response => {
        console.log("Successfully signed in!");
        console.log(response);
      },
      error: error => {
        console.log("Error while signing in");
        console.log(error);
      }
    });
  }
}
