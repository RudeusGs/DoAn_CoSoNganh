import baseApi from './base.api';

const homeApi = {
    getAllGameAccounts: async () => {
        return await baseApi.get('GameAccount/get-all');
    },
    getById: async (id: number | string) => {
        return await baseApi.get(`GameAccount/get-by-id?id=${id}`);
    },
    BuyGameAccount: async (gameAccountId: number, userId: number) => {
        const requestBody = {
            GameAccountId: gameAccountId,
            UserId: userId
        };
        return await baseApi.post('GameAccount/Purchase', requestBody);
    },
}    

export default homeApi;
