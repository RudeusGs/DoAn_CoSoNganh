

import baseApi from './base.api';

const homeApi = {
    getAllGameAccounts: async () => {
        return await baseApi.get('GameAccount/get-all');
    },
    getById: async (id: number | string) => {
        return await baseApi.get(`GameAccount/get-by-id?id=${id}`);
    },
};

export default homeApi;
