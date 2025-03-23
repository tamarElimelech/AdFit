import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Advertisement } from '../Models/advertisment.model';
import { Page } from '../Models/page.model';



@Injectable({
    providedIn: 'root',
  })

  export class PageService{

     constructor(private _httpClient: HttpClient){};

     getArrangePages():Observable<Page[]>{
        console.log("hello ");
        
        return this._httpClient.get<Page[]>(
            'https://localhost:7194/api/Page/ArrangePages'
        )
     }

sendEmailsToCust():Observable<any>{
        return this._httpClient.post<any>('https://localhost:7194/api/Page/sendEmailsToCustomers',{ observe: 'response' })
            
}
  }
    