import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserProfileComponent } from './componnents/user-profile/user-profile.component';
import { ManegerComponent } from './componnents/maneger/maneger.component';
export const routes: Routes = [
  { path: 'user-profile', component: UserProfileComponent },
  { path: 'manager-profile', component: ManegerComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
})
export class UserRoutingModule { }
