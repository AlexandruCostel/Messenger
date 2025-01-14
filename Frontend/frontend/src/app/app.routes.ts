import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MainPageComponent } from './main-page/main-page.component';
import { FriendRequestsComponent } from './friend-requests/friend-requests.component';
import { FriendsComponent } from './friends/friends.component';
import { ChatComponent } from './chat/chat.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'main-page', component: MainPageComponent },
    { path: 'friend-requests', component: FriendRequestsComponent },
    { path: 'friends', component: FriendsComponent },
    { path: 'chat', component: ChatComponent },
];
