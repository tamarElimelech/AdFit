import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PriceService } from '../service/prices.service';
import { AdvertisementAdminComponent } from '../advertisement/components/advertisement-admin/advertisement-admin.component';
import { Price } from '../Models/price.model';
import { ESize } from '../Models/advertisment.model';

@Component({
  selector: 'app-prices',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './prices.component.html',
  styleUrl: './prices.component.css'
})
export class PricesComponent {
  constructor(private _priceService:PriceService,private _adsAdminComp:AdvertisementAdminComponent){}
  selectedIndex:number=-1;
  newPrice!:Price
  prices:Price[]=[
    {
      id:-1,
      size: ESize.FULL,
      adPrice:100   
    },
    {
      id:-1,
      size: ESize.HALF,
      adPrice:80   
    },
    {
      id:-1,
      size: ESize.QUARTER,
      adPrice:50   
    },
    {
      id:-1,
      size: ESize.EIGHTH,
      adPrice:20   
    }
  ];
  
  ngOnInit(): void {
    
    this._priceService.getPrices().subscribe({
      next:(res)=>{
        console.log(res);
        if(res && Array.isArray(res) && res.length == 4){
           this.prices=res;
        }
       else if(res){
            this.prices.forEach(e=>{
              if(Array.isArray(res)){
                 res.forEach(i=>{
            if(e.size==i.size){
              e.adPrice=i.adPrice
              e.id=i.id
            }
          })
              }  
          }
        )
       }
      },
      error:(err:any)=>{
    console.log("error prices",err);
   }
    })
    
  }
  
  update(index:number){
  this.selectedIndex=index
  this.newPrice={
    ...this.prices[index],
    adPrice:0
  }
  }
  
  onSubmit (index:number){
    if(this.prices[index].id==-1){
      this._priceService.addPrices(this.newPrice).subscribe({
        next: (res:Price)=>{
         this.selectedIndex=-1
         this.prices[index].adPrice=res.adPrice
        },
        error:(err:any)=>{
  console.log("error to update price",err);
        }
    })}
    else{
      this._priceService.updatePrices(this.prices[index].id,this.newPrice).subscribe({
        next:(res:Price)=>{
         this.selectedIndex=-1
         this.prices[index].adPrice=res.adPrice
        },
        error:(err:any)=>{
  console.log("error to update price",err);
        }
  
    })
  }}
  cancelUpdate() {
    this.selectedIndex = -1;
    this.newPrice = {} as Price;
  }
  
  getSizeLabel(s:number){
   return this._adsAdminComp.getSizeLabel(s)
  }
}





