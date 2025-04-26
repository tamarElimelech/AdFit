import { Routes } from '@angular/router';
import { SignupComponent } from './auth/signup/signup.component';
import { LoginComponent } from './auth/login/login.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AddAdvertisementComponent } from './advertisement/components/add-advertisement/add-advertisement.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { NewspaperComponent } from './newspaper/newspaper.component';
import { UserAdvertisementComponent } from './advertisement/components/user-advertisement/user-advertisement.component';
import { AdvertisementAdminComponent } from './advertisement/components/advertisement-admin/advertisement-admin.component';
import { PricesComponent } from './prices/prices.component';
import { authGuard } from './auth.guard';

export const routes: Routes = [
    {path:'', component:LoginComponent},
    {path:'signup', component:SignupComponent},
    {path:'login', component:LoginComponent},
    {path:'home',component:HomePageComponent, canActivate:[authGuard]},
    {path:'addAdvertisement',component:AddAdvertisementComponent, canActivate:[authGuard]},
    {path:'logout',component:LogoutComponent,canActivate:[authGuard] },
    {path:'newspaper',component:NewspaperComponent, canActivate:[authGuard]},
    {path:'adsvertisementByUser' ,component:UserAdvertisementComponent, canActivate:[authGuard]},
    {path:'advertisementAdmin',component:AdvertisementAdminComponent, canActivate:[authGuard]},
    {path:'prices', component:PricesComponent, canActivate:[authGuard]},
    { path:'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule) , canActivate:[authGuard]},

];
