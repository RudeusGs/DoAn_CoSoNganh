// profile.api.js
import baseApi from './base.api';

const profile = {
    getAllProfile: async () => {
        return await baseApi.get('AccountWebsite/get-all');
    },
    getByIdProfile: async (id: number) => {
        return await baseApi.get(`AccountWebsite/get-by-id?id=${id}`);
    },
    getGameAccountsByUser: async (userId: number) => {
        try {
          return await baseApi.get(`GameAccount/get-by-user/${userId}`);
        } catch (error) {
          console.error('Error fetching purchased accounts:', error);
          throw error;
        }
      },
};

export default profile;
