import baseApi from './base.api';

const purchasedaccountApi = {
    getAllPurchasedAccount: async () => {
        return await baseApi.get('PurchasedAccount/get-all');
    },
}    

export default purchasedaccountApi;
