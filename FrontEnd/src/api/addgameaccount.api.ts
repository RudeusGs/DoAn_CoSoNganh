//addgameaccount.api.ts
import apiClient from './base.api'; 

export const addGameAccount = async (formData: FormData) => {
  try {
    const response = await apiClient.postForm('gameaccount/add', formData);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data?.message || 'An error occurred while adding the game account');
  }
};
