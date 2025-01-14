import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChatService } from '../Services/chat.service';
import { RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chat',
  standalone: true,
  templateUrl: './chat.component.html',
  imports: [FormsModule,CommonModule,RouterModule],
  styleUrls: ['./chat.component.css'],
  providers: [ChatService]
})
export class ChatComponent implements OnInit {
  message: string = '';
  public messages: { text: string; own: boolean }[] = [];
  private recipient: string = '';

  constructor(public chatService: ChatService,private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.recipient = params['user2Name'] || 'defaultUser';
      if (this.recipient.trim()) {
        this.chatService.connect(this.recipient);
        
      } else {
        console.warn('Recipient is not specified or invalid.');
      }
    });

    this.chatService.messages.subscribe((rawMessage) => {
      try {
        const parsedMessage = JSON.parse(rawMessage);
        this.messages.push({ text: parsedMessage.message, own: false });
      } catch {
        console.error('Failed to parse message:', rawMessage);
      }
    });
  }

  sendMessage() {
    if (this.message.trim()) {
      const fullMessage = JSON.stringify({
        recipient: this.recipient,
        message: this.message,
      });
      this.chatService.sendMessage(fullMessage);
      this.messages.push({ text: this.message, own: true });
      this.message = '';
    }
  }
}