import type { AxiosResponse } from 'axios';
import baseApi from './base.api';

const commentApi = {
    getAllComment: async () => {
        return await baseApi.get('CommentOrLike/get-all');
    },
    getById: async (id: number | string) => {
        return await baseApi.get(`CommentOrLike/get-by-id?id=${id}`);
    },
    addComment: async (formData: FormData): Promise<AxiosResponse> => {
        return await baseApi.postForm('CommentOrLike/add', formData);
    },
    
    updateComment: async (formData: FormData): Promise<AxiosResponse> => {
        return await baseApi.postForm('CommentOrLike/update', formData);
    },
    
    deleteComment: async (id: number | string): Promise<AxiosResponse> => {
        return await baseApi.delete(`CommentOrLike/delete?id=${id}`);
    },
}    

export default commentApi;