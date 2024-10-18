import baseApi from './base.api';

const AuctionApi = {
  getallAuction: async () => {
    try {
      return await baseApi.get('Auction/get-all'); // Đảm bảo URL đúng và hoạt động
    } catch (error) {
      throw new Error('API call failed'); // Xử lý lỗi API
    }
  },
};

export default AuctionApi;
