import { Component, OnInit } from '@angular/core';
import { FriendshipService } from '../Services/friendship-service.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-friends',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  providers: [FriendshipService],
  templateUrl: './friends.component.html',
  styleUrl: './friends.component.css',
  
})
export class FriendsComponent implements OnInit {
  MessageFriend(friendName: string): void {
    this.router.navigate(['/chat'], { queryParams: { user2Name: friendName } });
  }
  
  friends: any[] = [];

  constructor(private friendshipService: FriendshipService , private router:Router) {}

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