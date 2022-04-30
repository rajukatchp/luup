import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';
import { TranslateService } from '@ngx-translate/core';
import { LoginService } from './login.service';

// https://luupwebapi.azurewebsites.net/api/Workflows/GetWorkflows
// https://luupwebapi.azurewebsites.net/api/Workflows/GetWorkflowById/{Id}
// https://luupwebapi.azurewebsites.net/api/Workflows/GetWorkflowByName/{WorkflowName}
// POST
// https://luupwebapi.azurewebsites.net/api/Workflows
// UPDATE
// https://luupwebapi.azurewebsites.net/api/Workflows/{id}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(
    private router: Router,
    private translateService: TranslateService,
    private msalService: MsalService,
    private loginService: LoginService
  ) {}

  ngOnInit(): void {
    //this.msalService.instance.getA
  }

  isLoggedIn(): boolean {
    return this.msalService.instance.getActiveAccount() !== null;
  }


  login(): void {
    //uncomment this to run local
    // this.router.navigate(['notificatons'], {
    //   skipLocationChange: true
    // });
    // this.loginService.loginStatusUpdated.emit('USER_LOGGEDIN');

    //uncomment this to run deployment
    this.msalService
      .loginPopup()
      .subscribe((response: AuthenticationResult) => {
        this.msalService.instance.setActiveAccount(response.account);

        if(response.account !== null) {
            console.log(response.account.name)
            this.loginService.setLoginName(response.account.name)
            this.loginService.setUserEmail(response.account.username);;
        }
        this.loginService.loginStatusUpdated.emit('USER_LOGGEDIN');
        this.router.navigate(['notificatons'],{
          skipLocationChange: true,
        });
      });
  }

  LoginClickHandler(): void {
    this.login();
  }

  logout(): void {
    this.msalService.logout();
  }
}
