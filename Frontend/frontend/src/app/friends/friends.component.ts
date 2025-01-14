import { Component, OnInit } from '@angular/core';
import { FriendshipService } from '../Services/friendship-service.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-friends',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  providers: [FriendshipService],
  templateUrl: './friends.component.html',
  styleUrl: './friends.component.css',
  
})
export class FriendsComponent implements OnInit {
  friends: any[] = [];

  constructor(private friendshipService: FriendshipService) {}

  ngOnInit(): void {
    this.loadFriends();
  }

  loadFriends(): void {
    this.friendshipService.getFriends().subscribe({
      next: (friends) => {
        this.friends = friends;
      },
      error: (err) => {
        console.error('Failed to load friends', err);
      }
    });
  }

  deleteFriend(friendName: string): void {
    this.friendshipService.deleteFriend(friendName).subscribe({
      next: (response) => {
        console.log('Friend removed', response);
        this.loadFriends();
      },
      error: (err) => {
        console.error('Failed to remove friend', err);
      }
    });
  }
}