import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private socket!: WebSocket;
  public messages: Subject<string> = new Subject<string>();

  constructor(private http: HttpClient) {}

  connect(user2Name: string) {
    const url = `ws://localhost:5235/chat/connect?user2Name=${user2Name}`;
    this.socket = new WebSocket(url);

    this.socket.onmessage = (event: MessageEvent) => {
      console.log('Mesaj primit:', event.data);
      this.messages.next(event.data);
    };

    this.socket.onopen = () => {
      console.log('Conexiune WebSocket deschisă cu', user2Name);
    };

    this.socket.onclose = () => {
      console.log('Conexiune WebSocket închisă');
    };

    this.socket.onerror = (error) => {
      console.error('Eroare WebSocket:', error);
    };
  }

  sendMessage(message: string) {
    if (this.socket && this.socket.readyState === WebSocket.OPEN) {
      this.socket.send(message);
    } else {
      console.error('WebSocket nu este deschis!');
    }
  }
  getLastMessages(chatId: string, limit: number = 30): Observable<any[]> {
    return this.http.get<any[]>(`/chat/${chatId}/messages?limit=${limit}`);
  }
}