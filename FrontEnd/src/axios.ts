import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://localhost:7224/api/',
});

export default instance;
