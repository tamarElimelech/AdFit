//post model??

import { Advertisement, AdvertisementDTO } from "./advertisment.model";

export class Page{
    id!:number;
    pageNumber!:number;
    advertisements: AdvertisementDTO[] = [];
    capacity!:number; 
}

