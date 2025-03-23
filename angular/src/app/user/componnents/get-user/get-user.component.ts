import { Component } from '@angular/core';
import { UserService } from '../../../service/user.service';
import { User } from '../../../Models/user.model';
import { UserModule } from '../../user.module';


@Component({
  selector: 'app-get-user',
  templateUrl: './get-user.component.html',
  styleUrls: ['./get-user.component.css']
})

export class GetUserComponent {
    constructor(private _userService: UserService){};
    users!: User[];

    ngOnInit():void{
      this._userService.getUsers().subscribe({

        next:(res:any)=>{
          console.log("users:",res);
          this.users=res;
        },
        error:(err:any)=>{
          console.log("errrrrr",err);
        }
      })
    }
}
