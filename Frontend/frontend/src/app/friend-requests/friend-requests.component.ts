import { Component, OnInit } from '@angular/core';
import { FriendshipService } from '../Services/friendship-service.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-friend-requests',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule],
  providers: [FriendshipService],
  templateUrl: './friend-requests.component.html',
  styleUrl: './friend-requests.component.css'
})
export class FriendRequestsComponent implements OnInit {
  friendRequests: string[] = [];
  newFriendName: string = '';
  newRequestName: string = '';

  constructor(private friendshipService: FriendshipService) {}

  ngOnInit() {
    this.loadFriendRequests();
  }

  sendFriendRequest(): void {
    this.friendshipService.sendRequest(this.newRequestName).subscribe({
      next: (response) => {
        console.log('Friend request sent', response);
        this.newRequestName = '';
          this.loadFriendRequests();
      },
      error: (err) => {
        console.error('Failed to send request', err);
      }
    });
  }

  loadFriendRequests(): void {
    this.friendshipService.getFriendRequests().subscribe({
      next: (requests) => {
        this.friendRequests = requests;
      },
      error: (err) => {
        console.error('Failed to load friend requests', err);
      }
    });
  }

  acceptRequest(friendName: string): void {
    console.log('Accepting request for:', friendName);
    if (!friendName) {
      console.error('Friend name is invalid');
      return;
    }
    this.friendshipService.acceptRequest(friendName).subscribe({
      next: (response) => {
        console.log('Friend request accepted', response);
        this.loadFriendRequests();  
      },
      error: (err) => {
        console.error('Failed to accept request', err);
      }
    });
  }
  

  deleteRequest(friendName: string): void {
    this.friendshipService.deleteRequest(friendName).subscribe({
      next: (response) => {
        console.log('Friend request deleted', response);
        this.loadFriendRequests();
      },
      error: (err) => {
        console.error('Failed to delete request', err);
      }
    });
  }
}
