import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  loginName : string;
  constructor(
    private loginService: LoginService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loginName = this.loginService.getLoginName();

  }

  logOut(): void {

    this.loginService.logout();

  }

  routeToHomePage(): void{
    this.router.navigate(['notificatons'], {
      skipLocationChange: true,
    })
  }

  routeToCreateNotificaton(): void {
    this.router.navigate(['create-notification'], {
      skipLocationChange: true,
    });
  }

  routeToCreateCard(): void {
    this.router.navigate(['create-card'], {
      skipLocationChange: true,
    });
  }
}
