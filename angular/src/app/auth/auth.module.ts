import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { LogoutComponent } from './logout/logout.component';
import { LoginComponent } from './login/login.component';



@NgModule({
  declarations: [SignupComponent,LogoutComponent,LoginComponent],
  imports: [
    CommonModule,
   FormsModule,
   RouterLink,
  ],
})
export class AuthModule { }
