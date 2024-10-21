// profile.api.js
import baseApi from './base.api';

const profile = {
    getAllProfile: async () => {
        return await baseApi.get('AccountWebsite/get-all');
    },
    getByIdProfile: async (id: number) => {
        return await baseApi.get(`AccountWebsite/get-by-id?id=${id}`);
    },
};

export default profile;
