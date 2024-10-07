import baseApi from './base.api';

const notificationApi = {
    getAllNotifications: async () => {
        return await baseApi.get('Notification/get-all');
    },
    getNotificationById: async (id: number | string) => {
        return await baseApi.get(`Notification/get-by-id?id=${id}`);
    },
    addNotification: async (notificationData: { senderId: string, receiverId: string, title: string, message: string }) => {
        return await baseApi.post('Notification/add', notificationData);
    },
    updateNotification: async (notificationData: { id: number, title?: string, message?: string }) => {
        return await baseApi.put('Notification/update', notificationData);
    },
    deleteNotification: async (id: number | string) => {
        return await baseApi.delete(`Notification/delete?id=${id}`);
    },
};

export default notificationApi;
