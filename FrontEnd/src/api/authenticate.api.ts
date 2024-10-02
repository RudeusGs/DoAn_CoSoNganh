import baseApi from './base.api';

export default {
    login: async (model: LoginModel) : Promise<any|undefined> => {
      const response= await baseApi.postAuthenticate('Authenticate/login', model);
      if(response.status == 200){
      return response.data ;
      }
    },

    logout:()=>{
        //baseApi.postAuthenticate("Authenticate/Logout",null);
    },
    register: async(userName: string, password: string, name: string, email: string)=>{
      return await baseApi.postAuthenticate('Authenticate/register', {
        userName,
        password,
        fullName: name,
        email,
      });
    }
};

export interface LoginModel{
    userName: string,
    password: string,
  }
  
 export  interface UserInfoModel{
    name:string,
    blance:Number,
    userId:Number
  }
  
 export  interface LoginResponseModel{
    userInfo: UserInfoModel,
    email: string,
    token: string,
    expiration: string,
    roles: string[]
  }
