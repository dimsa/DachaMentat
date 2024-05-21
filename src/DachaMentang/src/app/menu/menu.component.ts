import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({

  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],

})
export class MenuComponent {
  constructor(private authService: AuthService) {

  }

  public get isAuth() {
    return this.authService.isAuth;
  }

}
