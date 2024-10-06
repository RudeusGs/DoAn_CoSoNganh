import baseApi from './base.api';

const homeApi = {
    getAllGameAccounts: async () => {
        return await baseApi.get('GameAccount/get-all');
    },
};

export default homeApi;
