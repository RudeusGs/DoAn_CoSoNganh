// gameaccount-model.ts
export interface AddGameAccountModel {
  id: number
  accountName: string;        
  accountPassword: string;    
  status: boolean;     
  content: string;        
  server: string;            
  earring?: boolean;          
  planet: string;              
  price: number;               
  files?: File[];              
  posterName: string;           
  createdDate: string;    
}