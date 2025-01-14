import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms';
import { RegisterRequestM } from '../models/auth-request.model';
import { AuthService } from '../Services/auth.service';
import { HttpClientModule } from '@angular/common/http'; //nu merge fara el
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,HttpClientModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  providers: [AuthService]
})
export class RegisterComponent {
  
  RegisterForm = new FormGroup({
    username: new FormControl('', Validators.required),
    email: new FormControl('',Validators.required),
    password: new FormControl('', Validators.required),
  });
  constructor(private authService: AuthService, private router: Router){};

  goToLogin() {
    this.router.navigate(['/login']);
  }

  register(){
    if(this.RegisterForm.invalid) return;
    const registerRequest: RegisterRequestM = {
      username: this.RegisterForm.value.username || '',
      email: this.RegisterForm.value.email || '',
      password: this.RegisterForm.value.password || ''
    };

    this.authService.register(registerRequest).subscribe(
      (response: any) => {
        //redirectionez undeva
        alert('Register successful');
      },
      (error: any) => {
        alert('Username or Email is used!');
      }
    );
  }
}
