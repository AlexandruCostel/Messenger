import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FriendshipService {
  private apiUrl = 'http://localhost:5235';

  constructor(private http: HttpClient) {}

  getFriendRequests(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/friend-requests`, {
      withCredentials: true
    });
  }

  getFriends(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/friends`, {
      withCredentials: true
    });
  }

  sendRequest(FriendName: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/send-request`, { FriendName }, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true,
    });
  }
  

  acceptRequest(friendName: string): Observable<any> {
    console.log('Accepting request for:', friendName);
    return this.http.post(`${this.apiUrl}/accept-request`, { friendName }, {
      withCredentials: true
    });
  }
  

  deleteRequest(friendName: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete-request`, {
      withCredentials: true,
      body: { friendName }
    });
  }
  
  deleteFriend(friendName: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete-friend`, {
      withCredentials: true,
      body: { friendName }
    });
  }
  

}