import { io } from 'socket.io-client';

const socket = io('https://localhost:7224/api/');

export default socket;
