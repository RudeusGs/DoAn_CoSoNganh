// src/models/auction.models.ts

export interface AddAuctionModel {
    auctionName: string;
    prize: string;
    startPrice: string;
    timeAuction: string; // Format: hh:mm:ss
    startDateTime: string; // DateTime format
    files: File[] | null; // Assuming you want to upload files
}

export interface UpdateAuctionModel {
    id: number; // ID of the auction to update
    auctionName?: string;
    prize?: string;
    startPrice?: string;
    timeAuction?: string; // Format: hh:mm:ss
    startDateTime?: string; // DateTime format
    files?: File[] | null; // Optional
}

export interface UpdateCurrentPriceModel {
    id: number; // ID of the auction to update the price for
    currentPrice: string; // New current price as string
}
