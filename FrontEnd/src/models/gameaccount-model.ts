//gameaccount-model.ts
export interface AddGameAccountModel {
    accountName: string;        
    accountPassword: string;    
    status: boolean;            
    server: string;           
    earring?: boolean;         
    planet: string;             
    price: number;             
    files?: File[];           
  }