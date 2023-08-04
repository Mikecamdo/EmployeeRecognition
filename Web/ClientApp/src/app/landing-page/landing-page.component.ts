import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginCredential, LoginResponse, SignupResponse, UserDto, UsersService } from '../services/users.service';
import { ToastrService } from 'ngx-toastr';

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
     private router: Router, private toastr: ToastrService) { }
  
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
      avatarUrl: "https://api.dicebear.com/6.x/miniavs/svg?seed=" + this.signUpName,
      bio: "I'm " + this.signUpName + ". Thanks for visiting my profile!"
    }
    this.usersService.addUser(newUser).subscribe({
      next: (response:SignupResponse) => {
        localStorage.setItem("token", response.token);
        this.modalService.dismissAll();
        this.router.navigate(['/home']);
      },
      error: error => {
        this.toastr.error(error.error.errorMessage, "Error while signing up");
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
        localStorage.setItem("token", response.token);
        this.modalService.dismissAll();
        this.router.navigate(['/home']);
      },
      error: error => {
        this.toastr.error(error.error.errorMessage, "Error while signing in");
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
