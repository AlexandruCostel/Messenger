import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../Services/auth.service';
import { AuthRequestM } from '../models/auth-request.model';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http'; 

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [ReactiveFormsModule,HttpClientModule],
  providers: [AuthService]
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  constructor(private authService: AuthService , private router: Router) {}
  
  login() {
    if (this.loginForm.invalid) return;
    const loginRequest: AuthRequestM = {
      username: this.loginForm.value.username || '',
      password: this.loginForm.value.password || ''
    };

    this.authService.login(loginRequest).subscribe(
      (response: any) => {
        //redirectionez undeva
        alert('Login successful');
      },
      (error: any) => {
        alert('Username or Password is not correct!');
      }
    );
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}

