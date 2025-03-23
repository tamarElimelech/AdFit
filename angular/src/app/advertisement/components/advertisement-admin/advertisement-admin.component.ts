
import { Component, Injectable, OnInit } from '@angular/core';
import { AdService } from '../../../service/advertisement';
import { Advertisement, AdvertisementDTO, ESize } from '../../../Models/advertisment.model';
import { AdvertisementModule } from '../../advertisement.module'; 
import { User } from '../../../Models/user.model';

@Component({
  selector: 'app-advertisement-admin',
  templateUrl: './advertisement-admin.component.html',
  styleUrl: './advertisement-admin.component.css'
})


@Injectable({
    providedIn: 'root',
  })

  
export class AdvertisementAdminComponent implements OnInit {
  advertisements!: AdvertisementDTO[];
  userData: any;
  user!: User;
  selectedAdIndex: number = -1;
  newAdvertisement!: Advertisement;

  constructor(private _adService: AdService) {}

  ngOnInit(): void {
    this.userData = localStorage.getItem('user');
    this.user = JSON.parse(this.userData);

    this._adService.getAdminAdvertisement().subscribe({
      next: (res) => {
        console.log("adminnnnn", res);
        if (res && Array.isArray(res) && res.length == 4) {
          this.advertisements = res;
          console.log("if"); 
        } else {
          console.log("else");
          this.advertisements = [
            {
              userId: this.user.id,
              image: 'https://i.fbcd.co/products/resized/resized-750-500/563d0201e4359c2e890569e254ea14790eb370b71d08b6de5052511cc0352313.jpg',
              size: ESize.EIGHTH,
              numOfWeeks: 0
            },
            {
              userId: this.user.id,
              image: 'https://i.fbcd.co/products/resized/resized-750-500/563d0201e4359c2e890569e254ea14790eb370b71d08b6de5052511cc0352313.jpg',
              size: ESize.FULL,
              numOfWeeks: 0         
            },
            {
              userId: this.user.id,
              image: 'https://i.fbcd.co/products/resized/resized-750-500/563d0201e4359c2e890569e254ea14790eb370b71d08b6de5052511cc0352313.jpg',
              size: ESize.HALF,
              numOfWeeks: 0
            },
            {
              userId: this.user.id,
              image: 'https://i.fbcd.co/products/resized/resized-750-500/563d0201e4359c2e890569e254ea14790eb370b71d08b6de5052511cc0352313.jpg',
              size: ESize.QUARTER,
              numOfWeeks: 0
            }
          ];
          
          this.advertisements.forEach(element => {
            res.forEach(item => {
              if (item.size == element.size) {
                element.image = item.image;
                element.numOfWeeks = item.numOfWeeks;
              }
            });       
          });
        }
        console.log("ather array", this.advertisements);
      },
      error: (err: any) => {
        console.log("adminError", err);
      }
    });
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

  update(index: number) {
    this.selectedAdIndex = index;
    this.newAdvertisement = {
      ...this.advertisements[index],
      image: null
    };
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.newAdvertisement.image = file;
    }
  }

  onSubmit(index: number) {
    if (!this.userData) {
      console.log("err in localstorage");
      return;
    }

    this.user = JSON.parse(this.userData);
    this.newAdvertisement.userId = this.user.id;
    this.newAdvertisement.size = this.advertisements[index].size;
    
    const formData = new FormData();
    formData.append('UserId', this.newAdvertisement.userId.toString()); 
    formData.append('ImageFile', this.newAdvertisement.image as Blob);
    formData.append('Size', this.newAdvertisement.size.toString()); 
    formData.append('NumOfWeeks', this.newAdvertisement.numOfWeeks.toString());

    this._adService.addAdminAdvertisement(formData).subscribe({
      next: (res) => {
        console.log("resadminnnnnnn", res);
        this.selectedAdIndex = -1; // Reset the form after successful submission
        // Update the local advertisement data
        if (res.image) {
          this.advertisements[index]=res.image;
        }
        this.advertisements[index].numOfWeeks = res.numOfWeeks;
      },
      error: (err: any) => {
        console.log("errrrrr", err);
      }
    });
  }

  cancelUpdate() {
    this.selectedAdIndex = -1;
    this.newAdvertisement = {} as Advertisement;
  }
}
