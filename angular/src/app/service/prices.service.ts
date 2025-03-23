import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Price } from "../Models/price.model";


@Injectable({
    providedIn: 'root',
  })

  export class PriceService{

    constructor(private _httpClient: HttpClient){};

    getPrices():Observable<Price[]>{
       return this._httpClient.get<Price[]>(
           'https://localhost:7194/api/Price'
       ) }

       updatePrices(id:number,p:Price):Observable<Price>{
        return this._httpClient.put<Price>(
            `https://localhost:7194/api/Price/${id}`,p
        ) }

        addPrices(p:Price):Observable<Price>{
            return this._httpClient.post<Price>(
                'https://localhost:7194/api/Price',p ) }



 }