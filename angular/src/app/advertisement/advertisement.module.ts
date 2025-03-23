import { NgModule } from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddAdvertisementComponent } from './components/add-advertisement/add-advertisement.component';
import { PopUpComponent } from './components/pop-up/pop-up.component';
import { AppComponent } from '../app.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AdvertisementAdminComponent } from './components/advertisement-admin/advertisement-admin.component';
import { UserAdvertisementComponent } from './components/user-advertisement/user-advertisement.component';
                                   

@NgModule({
  declarations: [AddAdvertisementComponent,PopUpComponent,AdvertisementAdminComponent,UserAdvertisementComponent
     ],
   imports: [
    CommonModule,
    FormsModule,
    NgClass,
    BrowserModule,
    NgbModule
       ],
      //  bootstrap: [AppComponent]
})
export class AdvertisementModule { }
