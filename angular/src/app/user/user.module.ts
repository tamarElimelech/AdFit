import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule } from '@angular/router';
import { UserProfileComponent } from './componnents/user-profile/user-profile.component';
import { GetUserComponent } from './componnents/get-user/get-user.component';
import { PopUpComponent } from './componnents/pop-up/pop-up.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { ManegerComponent } from './componnents/maneger/maneger.component';



@NgModule({
  declarations: [UserProfileComponent,GetUserComponent,PopUpComponent,ManegerComponent],
  imports: [
    CommonModule,
    RouterModule, 
    NgbModule,
     BrowserModule,
     RouterLink
  ]
})
export class UserModule {}
