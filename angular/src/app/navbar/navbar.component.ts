import { ChangeDetectorRef, Component , OnInit, inject} from '@angular/core';
import { RouterLink } from '@angular/router';
import { ShowIfManagerDirective } from '../show-if-manager.directive';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, ShowIfManagerDirective],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  isManeger:boolean=false;
  userData:any;
  userEmail!:string;

  ngOnInit():void{
    this.userData=localStorage.getItem('user');
    this.userEmail=JSON.parse(this.userData).email;
  }



}