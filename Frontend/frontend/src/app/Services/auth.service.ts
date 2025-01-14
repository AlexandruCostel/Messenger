import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequestM,RegisterRequestM } from '../models/auth-request.model'; // Adjust based on where you store this model


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseURL = 'http://localhost:5235/';
  private loginURL = 'http://localhost:5235/login';
  private registerURL = 'http://localhost:5235/register';
  private mainPageUrl = 'http://localhost:5235/mainpage';


  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(private http: HttpClient) {}

  login(loginRequest: LoginRequestM): Observable<any> {
    return this.http.post(this.loginURL, loginRequest, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
    });
  }
  register(registerRequest:RegisterRequestM):Observable<any>{
    return this.http.post(this.registerURL,registerRequest,this.httpOptions);
  }
  mainPage(): Observable<any> {
    return this.http.get(this.mainPageUrl, { withCredentials: true });
  }
}
