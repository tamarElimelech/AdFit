import { Page } from "./page.model";
import { User } from "./user.model";
//post model??
export class Advertisement{
    static count: number = 0;
    userId!: number
    image?:File|null; 
    size !:ESize;
    numOfWeeks!:number;
}

export enum ESize {
    EIGHTH = 1,
    QUARTER = 2,
    HALF = 4,
    FULL = 8
  }

  export class AdvertisementDTO{
    static count: number = 0;
    userId!: number
    image: string = '';
    size !:ESize;
    numOfWeeks!:number;
    numOfAd?: any;
}

