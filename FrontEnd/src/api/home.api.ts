//đây là home.api.ts
import baseApi from './base.api';

const homeApi = {
    getAllGameAccounts: async () => {
        return await baseApi.get('GameAccount/get-all');
    },
    getbyid: async (id: number| string) => {
        return await baseApi.get('GameAccount/get-by-id?id='+id);
    },
};

export default homeApi;
