export interface LoginModel{
    user: string,
    password: string,
  }
  
  export interface UserInfoModel {
    userName: string;
    id: number;
    email: string;
    fullName?: string;
    balance: string;
    coin: number;
  }
  
  
 export  interface LoginResponseModel{
    userId:number,
    username:string,
    email: string,
    token: string,
    expiration: string,
    roles: string[]
  }