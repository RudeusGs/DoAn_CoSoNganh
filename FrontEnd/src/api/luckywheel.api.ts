// src/api/luckywheel.api.ts
import baseApi from './base.api';

export const luckyWheelApi = {
  // Lấy tất cả phần thưởng
  getAllLuckyWheel: async () => {
    try {
      const response = await baseApi.get('LuckyWheel/get-all');
      return response.data.result;
    } catch (error) {
      console.error('Error fetching Lucky Wheels:', error);
      return { isSuccess: false, message: 'Lỗi kết nối API' };
    }
  },

  spin: async () => {
    try {
      const response = await baseApi.post('LuckyWheel/spin', {});
      return response.data.result;
    } catch (error) {
      console.error('Error spinning the wheel:', error);
      return { isSuccess: false, message: 'Lỗi kết nối API' };
    }
  },
};
