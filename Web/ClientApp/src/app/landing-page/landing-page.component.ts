import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { LoginCredential, LoginResponse, SignupResponse, UserDto, UsersService } from '../services/users.service';

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
  
  constructor(private modalService: NgbModal, private usersService: UsersService,
     private router: Router) { }
  
  open(content: any, label: string) {
    const modalRef = this.modalService.open(content, {ariaLabelledBy: label, windowClass: 'custom-modal'});
    
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
      avatarUrl: "https://api.dicebear.com/6.x/miniavs/svg?seed=" + this.signUpName
    }
    this.usersService.addUser(newUser).subscribe({
      next: (response:SignupResponse) => {
        console.log("Successfully signed up!");
        console.log(response);
        localStorage.setItem("token", response.token);
        this.modalService.dismissAll();
        this.router.navigate(['/home']);
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
      next: (response:LoginResponse) => {
        console.log("Successfully signed in!");
        console.log(response);
        localStorage.setItem("token", response.token);
        this.modalService.dismissAll();
        this.router.navigate(['/home']);
      },
      error: error => {
        console.log("Error while signing in");
        console.log(error.error.errorMessage);
      }
    });
  }

  disableSignIn(): boolean {
    return !this.signInName || !this.signInPassword;
  }

  disableSignUp(): boolean {
    return !this.signUpName || !this.signUpPassword || !this.signUpConfirmPassword || this.invalidPassword();
  }

  invalidPassword(): boolean {
    if (this.signUpConfirmPassword !== '' && this.signUpConfirmPassword !== this.signUpPassword) {
      return true;
    }
    return false;
  }
}
