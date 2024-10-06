// src/api/luckywheel.api.ts
import axios from 'axios';
import baseApi from './base.api';
export const luckyWheelApi = {
    getAll: async () => {
        const response = await axios.get(`${baseApi}/get-all`);
        return response.data;
    },

    getById: async (id: number) => {
        const response = await axios.get(`${baseApi}/get-by-id`, { params: { id } });
        return response.data;
    },

    add: async (data: FormData) => {
        const response = await axios.post(`${baseApi}/add`, data, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data;
    },

    update: async (data: FormData) => {
        const response = await axios.put(`${baseApi}/update`, data, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data;
    },

    delete: async (id: number) => {
        const response = await axios.delete(`${baseApi}/delete`, { params: { id } });
        return response.data;
    }
};
