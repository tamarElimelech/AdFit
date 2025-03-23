


import { Advertisement } from "./advertisment.model";

export class User{
    id!:number;
    password!:string;
    name!:string; 
    email!:string;
    phone!:string;
    Role!:ERole 
    advertisements: Advertisement[] = [];
}

export enum ERole
{
    ADMIN,
    NEWSPAPER_OWNER,
    USER
}