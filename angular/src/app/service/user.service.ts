import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User } from '../Models/user.model';



@Injectable({
    providedIn: 'root',
  })

  export class UserService{
    

    constructor(private _httpClient: HttpClient){};


    isAuthenticated$ = new BehaviorSubject<boolean>(!!localStorage.getItem('user'));
    
    get isAuthenticated(): boolean {
        return this.isAuthenticated$.getValue();
      }


    login(user:any):Observable<any>{
            
        return this._httpClient.post<{token:string}>(
            'https://localhost:7194/api/Auth/login',user,{ observe: 'response' }
        ).pipe(
            tap((res:any)=>{
                console.log("res",res);
                localStorage.setItem('user',JSON.stringify({token:res.body.token,id:res.body.userId,email:res.body.userEmail}));
                this.isAuthenticated$.next(true);
            })
        );
    }


    signup(user:any):Observable<any>{
        return this._httpClient.post<{token:string}>(
            'https://localhost:7194/api/Auth/signUp', user , { observe: 'response' }
        ).pipe(
            tap((res:any)=>{
                console.log("res",res);
                localStorage.setItem('user',JSON.stringify({token:res.body.token,id:res.body.userId,email:res.body.userEmail}));
                this.isAuthenticated$.next(true);
            })
        )
    }


    getUserById(id:number):Observable<any>{
        console.log("serviceeeeeUserrrrr",id);
        
        return this._httpClient.get<any>(
            'https://localhost:7194/api/User/'+ id , { observe: 'response' }
        );
    }

    getUsers():Observable<User[]>{
        
        return this._httpClient.get<User[]>(
            'https://localhost:7194/api/User') }
        
  }

