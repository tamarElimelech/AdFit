import { NgModule } from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddAdvertisementComponent } from './components/add-advertisement/add-advertisement.component';
import { PopUpComponent } from './components/pop-up/pop-up.component';
import { AppComponent } from '../app.component';
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
    NgbModule,
    ReactiveFormsModule,
       ],
})
export class AdvertisementModule { }
