import { LEADING_TRIVIA_CHARS } from '@angular/compiler/src/render3/view/template';
import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  isUserLoogedIn: boolean = false;
  userName: string;
  userEmail: string;

  loginStatusUpdated = new EventEmitter<string>();
  constructor(private msalService: MsalService, private router: Router) {}

  updateLoginStatus(): void {
    this.loginStatusUpdated.emit();
  }

  isUserLoggedIn(): boolean {
    return this.msalService.instance.getActiveAccount !== null;
  }

  login() {
    //this.loginStatusUpdated.emit('USER_LOGGEDIN');
    this.msalService
      .loginPopup()
      .subscribe((response: AuthenticationResult) => {
        this.msalService.instance.setActiveAccount(response.account);
      });
  }

  setUserEmail(userEmail: any) {
    this.userEmail = userEmail;
  }

  getUserEmail(): string {
    return this.userEmail;
  }
  setLoginName(userName: any) {
    this.userName = userName;
  }

  getLoginName(): string {
    if (this.userName) {
      return this.userName;
    } else {
      return 'User'; // for local user only
    }
  }

  logout() {
    // this.loginStatusUpdated.emit('LOGGED_OUT');
    // this.router.navigate(['login'], {
    //   skipLocationChange: true,
    // });

    this.msalService.logout().subscribe((res) => {
      console.log('logging out', res);
      this.loginStatusUpdated.emit('LOGGED_OUT');
      this.router.navigate(['login'], {
        skipLocationChange: true,
      });
    });
  }
}
