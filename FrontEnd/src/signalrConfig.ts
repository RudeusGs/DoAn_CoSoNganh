
import * as signalR from '@microsoft/signalr';

const connection = new signalR.HubConnectionBuilder()
  .withUrl('https://localhost:7224/auctionHub')
  .withAutomaticReconnect()
  .build();

export default connection;
