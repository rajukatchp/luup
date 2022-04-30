import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { TranslateService } from '@ngx-translate/core';
import { LoginService } from './login/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'luup-app';
  isUserLoggedIn = false;
  constructor(
    private router: Router,
    private translateService: TranslateService,
    private msalService: MsalService,
    private loginService :LoginService
  ) {
    this.loginService.loginStatusUpdated.subscribe(res => {
      console.log(res)
      if(res === 'USER_LOGGEDIN') {
        this.isUserLoggedIn = true;
      }
      if(res === 'LOGGED_OUT') {
        this.isUserLoggedIn = false;
      }


    })
  }

  ngOnInit(): void {
    this.router.navigate(['login'],{
      skipLocationChange: true,
    });
  }


}
