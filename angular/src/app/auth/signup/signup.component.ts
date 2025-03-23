import { Component } from '@angular/core';
import { UserService } from '../../service/user.service';
import { Router} from '@angular/router';
import { AuthModule } from '../auth.module';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  
 
  constructor(private _userService:UserService,private _router: Router){};

  name: string = '';
  email: string = '';
  password: string = '';
  phone:string='';
  msg:string="";

  onSubmit(){
    
    this._userService.signup({ email:this.email, password: this.password ,phone:this.phone,name:this.name}).subscribe({

      next:(res:any)=>{

        if(res.status==200){
         
          this._router.navigate(['home']);
        }

      },

     error:(err:any)=>{
      if(err.status==409)
        {
          this.msg="email exsist"; 
          console.log("400"); 
        }

      console.log("err",err);
     },
    });
  }



}
