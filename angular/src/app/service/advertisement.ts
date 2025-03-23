import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Advertisement, AdvertisementDTO } from '../Models/advertisment.model';



@Injectable({
    providedIn: 'root',
  })

  export class AdService{
    

    constructor(private _httpClient: HttpClient){};


    
    addAdvertisement(advertisement:any):Observable<any>{

        console.log("serviceeee",advertisement);

        return this._httpClient.post<any>( 
            
           'https://localhost:7194/api/Advertisement', advertisement , {observe:'response'}
        );
    }

    getAdvertisements():Observable<Advertisement[]>{

        return this._httpClient.get<Advertisement[]>( 
                  ''
        );
    } 

    getEmpties():Observable<boolean[]>{
        return this._httpClient.get<boolean[]>('https://localhost:7194/api/Page/getEmpties')
    }

    getAllByUserId(id:number):Observable<Advertisement[]>{
        return this._httpClient.get<Advertisement[]>(`https://localhost:7194/api/Advertisement/getAllByUserId?id=${id}`)
    }

    getAdminAdvertisement():Observable<AdvertisementDTO[]>{
        return this._httpClient.get<AdvertisementDTO[]>(
            'https://localhost:7194/api/AdminAdvertisement'
        )
    }
    addAdminAdvertisement(advertisement:any):Observable<any>{

        console.log("serviceeeeAdminnn",advertisement);

        return this._httpClient.post<any>( 
            
           'https://localhost:7194/api/AdminAdvertisement', advertisement , {observe:'response'}
        );
    }

}