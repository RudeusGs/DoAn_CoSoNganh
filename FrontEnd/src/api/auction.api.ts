import baseApi from './base.api';

const homeApi = {
    getallAuction: async () => {
        return await baseApi.get('Auction/get-all');
    },
};

export default homeApi;
