import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Advertisement,ESize } from '../../../Models/advertisment.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AdvertisementModule } from '../../advertisement.module'; 
import { AdService } from '../../../service/advertisement';
import { PopUpComponent } from '../pop-up/pop-up.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PriceService } from '../../../service/prices.service';
import { Price } from '../../../Models/price.model';


@Component({

  selector: 'app-add-advertisement',
  templateUrl: './add-advertisement.component.html',
  styleUrls: ['./add-advertisement.component.css']
})
export class AddAdvertisementComponent implements OnInit {

  advertisement: Advertisement = new Advertisement();
  userData:any = localStorage.getItem('user');
   user:any;
  index!:number;
  sizes = [
    { value: ESize.EIGHTH, label: '1/8 Page',index:0 },
    { value: ESize.QUARTER, label: '1/4 Page',index:1 },
    { value: ESize.HALF, label: '1/2 Page',index:2 },
    { value: ESize.FULL, label: 'Full Page' ,index:3}
  ];
  
  prices!:Price[];
  sizeIndex!:number;
  constructor(private router: Router,private _adService:AdService,private modalService: NgbModal,private _pricesService:PriceService) {}

  ngOnInit() {
      this._pricesService.getPrices().subscribe({
        next:(res)=>{
          this.prices=res;
          console.log("prices",this.prices);
          
        },
        error:(err)=>{
          console.log("errrPricess ",err);
        }
      })
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.advertisement.image = file;
    }
  }


  openPopup() {
    this.modalService.open(PopUpComponent);
  }


getIndex(index:number){
  return Math.log2(index);
}

  onSubmit() {
    if(!this.userData)
      console.log("err in localstorage");

   this.user=JSON.parse(this.userData);
   this.advertisement.userId=this.user.id;
   console.log("advertisement",this.advertisement);
    
   const formData = new FormData();
   formData.append('UserId',this.advertisement.userId.toString()); 
   formData.append('ImageFile',this.advertisement.image as Blob);
   formData.append('Size',this.advertisement.size.toString()); 
   formData.append('NumOfWeeks',this.advertisement.numOfWeeks.toString());


   this._adService.addAdvertisement(formData).subscribe({
      next:(res:Advertisement)=>{
        console.log("res",res);
        this.openPopup();
        this.router.navigate(['/home'])
      },
      error:(err:any)=>{
        console.log("errrrrr",err);
      }
   })

  }
}


