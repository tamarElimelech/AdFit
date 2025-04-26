import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Advertisement, ESize } from '../../../Models/advertisment.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AdvertisementModule } from '../../advertisement.module'; 
import { AdService } from '../../../service/advertisement';
import { PopUpComponent } from '../pop-up/pop-up.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PriceService } from '../../../service/prices.service';
import { Price } from '../../../Models/price.model';

@Component({
  selector: 'app-add-advertisement',
  templateUrl: './add-advertisement.component.html',
  styleUrls: ['./add-advertisement.component.css'],
})
export class AddAdvertisementComponent implements OnInit {
  selectedFile: File | null = null;
  adForm!: FormGroup;
  userData: any = localStorage.getItem('user');
  user: any;
  prices!: Price[];
  sizes = [
    { value: ESize.EIGHTH, label: '1/8 Page', index: 0 },
    { value: ESize.QUARTER, label: '1/4 Page', index: 1 },
    { value: ESize.HALF, label: '1/2 Page', index: 2 },
    { value: ESize.FULL, label: 'Full Page', index: 3 }
  ];

  constructor(
    private router: Router,
    private _adService: AdService,
    private modalService: NgbModal,
    private _pricesService: PriceService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this._pricesService.getPrices().subscribe({
      next: (res) => {
        this.prices = res;
      },
      error: (err) => {
        console.log("errrPricess ", err);
      }
    });
  }

  private initForm() {
    this.adForm = new FormGroup({
      size: new FormControl('', [Validators.required]),
      numOfWeeks: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.max(52)
      ])
    });
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  openPopup() {
    this.modalService.open(PopUpComponent);
  }

  save() {
    if (!this.userData) {
      console.log("err in localstorage");
      return;
    }

    if (this.adForm.invalid || !this.selectedFile) {
      return;
    }

    this.user = JSON.parse(this.userData);
    const formData = new FormData();
    formData.append('UserId', this.user.id.toString());
    formData.append('ImageFile', this.selectedFile);
    formData.append('Size', this.adForm.get('size')?.value.toString());
    formData.append('NumOfWeeks', this.adForm.get('numOfWeeks')?.value.toString());

    this._adService.addAdvertisement(formData).subscribe({
      next: (res: Advertisement) => {
        this.openPopup();
        this.router.navigate(['/home']);
      },
      error: (err: any) => {
        console.log("errrrrr", err);
      }
    });
  }
}