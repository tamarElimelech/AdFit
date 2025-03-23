import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../service/user.service';
import { User } from '../../../Models/user.model';
import { Advertisement } from '../../../Models/advertisment.model';
import { AdService } from '../../../service/advertisement';

@Component({
  selector: 'app-user-advertisement',
  templateUrl: './user-advertisement.component.html',
  styleUrl: './user-advertisement.component.css'
})
export class UserAdvertisementComponent implements OnInit {

  userData:any;
  userId!:number;
  user!:User;
  advertisements!: Advertisement[];
  

  constructor(private _userService:UserService,private _adsService:AdService) {}

  ngOnInit() {
    this.userData=localStorage.getItem('user');
    this.userId=JSON.parse(this.userData).id;

this._adsService.getAllByUserId(this.userId).subscribe({
  next:(res:any)=>{
    this.advertisements=res;
    console.log("res",res);

  },
  error:(err:any)=>{
console.log("error to get bytes",err);

  }
 })

   console.log("ads",this.advertisements);
  }


  getSizeLabel(size: number): string {
    switch (size) {
      case 1: return 'Eighth Page';
      case 2: return 'Quarter Page';
      case 4: return 'Half Page';
      case 8: return 'Full Page';
      default: return 'Unknown';
    }
  }
  
    
}
