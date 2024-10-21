import axios from 'axios';

const baseUrl = 'https://localhost:7224/';

const getUserById = (userId: number) => {
  return axios.get(`${baseUrl}/${userId}`);
};

export default {
  getUserById,
};