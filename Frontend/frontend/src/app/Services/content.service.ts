import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContentService {
  private mainPageUrl = 'http://localhost:5235/MainPage';
  constructor(private http: HttpClient) {}

  getPosts(): Observable<any> {
    return this.http.get<any>(this.mainPageUrl, { withCredentials: true });
  }

}
