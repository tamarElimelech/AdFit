import { Component } from '@angular/core';
import { UserService } from '../../service/user.service';
import { Router } from '@angular/router';
import { AuthModule } from '../auth.module';


@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent {

  constructor(private _router:Router, private _userService:UserService){}

logout(){
    localStorage.removeItem('user'); // מוחק את הטוקן
    this._userService.isAuthenticated$.next(false);
    this._router.navigate(['/login']);
}
}
