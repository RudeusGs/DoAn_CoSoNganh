import baseApi from './base.api';

const purchasedaccountApi = {
  getPurchasedAccountsByUser: async (userId: number) => {
    try {
      return await baseApi.get(`PurchasedAccount/get-by-user/${userId}`);
    } catch (error) {
      console.error('Error fetching purchased accounts:', error);
      throw error;
    }
  },
};

export default purchasedaccountApi;
