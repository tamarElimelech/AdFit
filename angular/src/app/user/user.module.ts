import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule } from '@angular/router';
import { UserProfileComponent } from './componnents/user-profile/user-profile.component';
import { PopUpComponent } from './componnents/pop-up/pop-up.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ManegerComponent } from './componnents/maneger/maneger.component';
import { UserRoutingModule } from './user.routing.module';


@NgModule({
  declarations: [UserProfileComponent,PopUpComponent,ManegerComponent],
  imports: [
    CommonModule,
    RouterModule, 
    NgbModule,
     RouterLink,
     UserRoutingModule
  ]
})
export class UserModule {}
