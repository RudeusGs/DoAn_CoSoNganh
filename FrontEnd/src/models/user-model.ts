export interface LoginModel{
    user: string,
    password: string,
  }
  
 export  interface UserInfoModel{
    username:string,
    userId:Number,
    email: string,
    fullName?: string,
  }
  
 export  interface LoginResponseModel{
    userId:number,
    username:string,
    email: string,
    token: string,
    expiration: string,
    roles: string[]
  }