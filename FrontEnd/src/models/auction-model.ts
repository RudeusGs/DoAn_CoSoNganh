
export interface AuctionModel {
    id: number;
    prize: string;
    auctionName: string;
    startPrice: number;
    currentPrice: number;
    startDateTime: string;
    image: string;
    timeAuction: string;
    status: string;
    winner?: number;
  }
  
