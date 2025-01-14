import { Component, OnInit } from '@angular/core';
import { ContentService } from '../Services/content.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [HttpClientModule,CommonModule],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css',
  providers: [ContentService]
})

export class MainPageComponent implements OnInit {
goToAddFriends(arg0: string) {
  this.router.navigate(['/friend-requests']);
}
goToFriends(arg0: string) {
  this.router.navigate(['/friends']);
}
goToPage(arg0: string) {
throw new Error('Method not implemented.');
}
  
  posts: any[] = [];

  constructor(private contentService: ContentService, private router: Router) {}
  
  ngOnInit(): void {
    this.contentService.getPosts().subscribe(
      data => {
        this.posts = Array.isArray(data) ? data : [];
      },
      error => {
        this.posts = [];
      }
    );
    
  }

}
