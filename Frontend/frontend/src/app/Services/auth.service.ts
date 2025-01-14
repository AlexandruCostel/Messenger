import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthRequestM } from '../models/auth-request.model'; // Adjust based on where you store this model


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseURL = 'http://localhost:5235/';
  private loginURL = 'http://localhost:5235/login';
  private registerURL = 'http://localhost:5235/register';


  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(private http: HttpClient) {}

  login(loginRequest: AuthRequestM): Observable<any> {
    return this.http.post(this.loginURL, loginRequest, this.httpOptions);
  }
  register(registerRequest:AuthRequestM):Observable<any>{
    return this.http.post(this.registerURL,registerRequest,this.httpOptions);
  }
}
