import { Component , OnInit} from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  isManeger:boolean=false;
  userData:any;
  userEmail!:string;
  cdRef: any;

  ngOnInit():void{
    this.userData=localStorage.getItem('user');
    this.ifManager()
  }

  ifManager(){
    this.userEmail=JSON.parse(this.userData).email;
   if(this.userEmail.includes("manager")){
    this.isManeger=true
    // window.location.reload()
    this.cdRef.detectChanges();
   }
  }

}