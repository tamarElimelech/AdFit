import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appShowIfManager]',
  standalone:true
})
export class ShowIfManagerDirective implements OnInit {

  @Input('appShowIfManager') userEmail: string = '';

  constructor(private el: ElementRef) {}

  ngOnInit() {
    if (!this.userEmail.includes('manager')) {
      this.el.nativeElement.style.display = 'none';
    }
  }
}
