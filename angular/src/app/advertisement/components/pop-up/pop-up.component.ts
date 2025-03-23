import { Component, NgModuleRef, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-pop-up',
  templateUrl: './pop-up.component.html',
  styleUrl: './pop-up.component.css'
})
export class PopUpComponent{
 


  constructor(private modalService: NgbModal) {}

  modalRef!:NgbModalRef

  closePopup() {
    console.log('reffffffffffff');
    
       this.modalRef=this.modalService.open(PopUpComponent);
       this.modalRef.close();
  }
}
