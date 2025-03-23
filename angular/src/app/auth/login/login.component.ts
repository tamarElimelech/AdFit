import { Component, OnInit } from '@angular/core';
import { UserService } from '../../service/user.service';
import { Router} from '@angular/router';
import { AuthModule } from '../auth.module';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
   
  constructor(private _userService: UserService,private _router:Router) { }

  ngOnInit(): void {
    if(this._userService.isAuthenticated)
       this._router.navigate(['home']);
  }

  msg = "";
  email: string = '';
  password: string = '';


  onSubmit(){
    console.log("gggggggggggggggggggggg");
    
    this._userService.login({ email:this.email, password: this.password }).subscribe({

      next:(res:any)=>{
         console.log("hiiii",res);

        if(res.status==200){
         
          this._router.navigate(['home']);
        }
       
          
        
      
      },

     error:(err:any)=>{
      if(err.status==400)
        {
          this.msg="incorrect password"; 
          console.log("400"); 
       }

       else
       this.msg="user not exsist";


      console.log("err",err);
     },
    });
  }
}
