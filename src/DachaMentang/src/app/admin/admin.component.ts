import { Component } from '@angular/core';
import { FormsModule, NgForm } from "@angular/forms";
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

  login: string = "";

  password: string = "";
  lastError: string = "";

  public get isAuth() {
    return this.authService.isAuth;
  }

  public logout() {
    this.authService.logout();
  }

  public get errorMessage(): string {
      return this.lastError;
  }

  constructor(private authService: AuthService) {

  }

  checkToken() {
    this.authService.checkToken();
  }

  checkCors() {
    this.authService.checkCors();
  }

  async onSubmit() {
    try {
      await this.authService.Auth(this.login, this.password);
      this.lastError = "";
    } catch (error) {
      if (error instanceof Error) {
        this.lastError = error.message;
      }
    }
  }

}
