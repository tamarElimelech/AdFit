import { Component, Injectable, OnInit } from '@angular/core';
import { UserService } from '../../../service/user.service';
import { User } from '../../../Models/user.model';
import { UserModule } from '../../user.module';
import { AdService } from '../../../service/advertisement';
import { PopUpComponent } from '../pop-up/pop-up.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject } from 'rxjs';


@Component({
  selector: 'app-maneger',
  templateUrl: './maneger.component.html',
  styleUrl: './maneger.component.css'
})



@Injectable({
  providedIn: 'root'
})


export class ManegerComponent {
    constructor(private _userService:UserService,private modalService:NgbModal,private _popup:PopUpComponent){};
     
     userData:any;
     user!:User;  
   
    
     ngOnInit(): void {
   
      this.userData=localStorage.getItem('user');
      this.user=JSON.parse(this.userData);
   
       
       this._userService.getUserById(this.user.id).subscribe({
         next:(res:any)=>{
           console.log("userById",res);
            this.user=res.body;
         },
         error:(err:any)=>{
           console.log("userById",err);
         }
       })
     }
     
     
       openPopup() {
         this.modalService.open(PopUpComponent);
       }
   
      
     
}
