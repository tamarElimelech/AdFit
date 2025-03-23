import { Routes } from '@angular/router';
import { SignupComponent } from './auth/signup/signup.component';
import { LoginComponent } from './auth/login/login.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AddAdvertisementComponent } from './advertisement/components/add-advertisement/add-advertisement.component';
import { UserProfileComponent } from './user/componnents/user-profile/user-profile.component'; 
import { GetUserComponent } from './user/componnents/get-user/get-user.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { NewspaperComponent } from './newspaper/newspaper.component';
import { ManegerComponent } from './user/componnents/maneger/maneger.component';
import { UserAdvertisementComponent } from './advertisement/components/user-advertisement/user-advertisement.component';
import { AdvertisementAdminComponent } from './advertisement/components/advertisement-admin/advertisement-admin.component';
import { PricesComponent } from './prices/prices.component';

export const routes: Routes = [
    {path:'', component:LoginComponent},
    {path:'signup', component:SignupComponent},
    {path:'login', component:LoginComponent},
    {path:'home',component:HomePageComponent},
    {path:'addAdvertisement',component:AddAdvertisementComponent},
    {path:'user',component:UserProfileComponent},
    {path:'allUsers',component:GetUserComponent},
    {path:'logout',component:LogoutComponent},
    {path:'newspaper',component:NewspaperComponent},
    {path:'maneger',component:ManegerComponent},
    {path:'adsvertisementByUser' ,component:UserAdvertisementComponent},
    {path:'advertisementAdmin',component:AdvertisementAdminComponent},
    {path:'prices', component:PricesComponent}
];
