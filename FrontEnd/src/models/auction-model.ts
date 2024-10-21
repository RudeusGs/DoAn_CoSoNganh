export interface AddAuctionModel {
  auctionName?: string;
  prize?: string;
  files?: File[]; 
  image?: string;
  startPrice?: string;
  currentPrice?: string;
  startDateTime?: string;
  timeAuction?: string;
  status?: string;
  winner?: number;
}

export interface UpdateAuctionModel {
  id?: number;
  prize?: string;
  auctionName?: string;
  files?: File[];
  image?: string;
  startPrice?: string;
  startDateTime?: string;
  timeAuction?: string;
}

export interface UpdateCurrentPriceModel {
  id?: number;
  currentPrice?: string;
  winner?: number;
}

export interface AuctionModel {
  id: number;
  prize: string;
  auctionName: string;
  startPrice: string; // Chuyển thành string
  currentPrice: string;
  startDateTime: string;
  image: string;
  timeAuction: string;
  status: string;
  winner?: number;
  winnerName?: string;
}
