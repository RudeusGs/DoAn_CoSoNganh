//addgameaccount.api.ts
import baseapi from './base.api'; 

export const addGameAccount = async (formData: FormData) => {
    const response = await baseapi.postForm('gameaccount/add', formData);
    return response.data;
};
