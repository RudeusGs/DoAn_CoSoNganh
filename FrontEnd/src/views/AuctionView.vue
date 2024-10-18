<template>
    <div class="auction-list container mt-5 mb-5">
      <h2 class="text-center mb-4">Auction List</h2>
      
      <div v-if="loading" class="text-center">
        <p>Loading auctions...</p>
      </div>
      
      <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>
      
      <div v-if="auctions.length > 0" class="row g-4">
        <div class="col-md-6 col-lg-4" v-for="auction in auctions" :key="auction.id">
          <div class="auction-card card shadow-sm">
            <img :src="getImageUrl(auction.image)" class="card-img-top" alt="Auction Image" />
            <div class="card-body">
              <h5 class="card-title">{{ auction.auctionName }}</h5>
              <p class="card-text">Gía bắt đầu: {{ auction.startPrice }} VNĐ</p>
              <p class="card-text">Ngày bắt đầu: {{ formatDate(auction.startDateTime) }}</p>
              <button :class="buttonClass(auction)" class="btn w-100 mt-3">
                {{ buttonLabel(auction) }}
              </button>
            </div>
          </div>
        </div>
      </div>     
      <div v-else class="alert alert-info">No auctions available at the moment.</div>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, onMounted } from 'vue';
  import AuctionApi from '@/api/auction.api';
  import type { AuctionModel } from '@/models/auction-model';
  
  export default defineComponent({
    name: 'AuctionList',
    setup() {
      const auctions = ref<AuctionModel[]>([]);
      const loading = ref(true);
      const errorMessage = ref<string | null>(null);
  
      const fetchAuctions = async () => {
        try {
          const response = await AuctionApi.getallAuction();
          if (response.data.result && response.data.result.data.length > 0) {
            auctions.value = response.data.result.data as AuctionModel[];
          } else {
            errorMessage.value = 'No auctions found.';
          }
        } catch (error) {
          errorMessage.value = 'Failed to fetch auctions.';
        } finally {
          loading.value = false;
        }
      };
  
      const formatDate = (dateString: string) => {
        const date = new Date(dateString);
        return date.toLocaleDateString('vi-VN', {
          year: 'numeric',
          month: 'long',
          day: 'numeric',
        });
      };
  
      const getImageUrl = (imagePath: string) => {
        const baseUrl = 'https://localhost:7224/';
        return `${baseUrl}${imagePath}`;
      };
  
      const buttonClass = (auction: AuctionModel) => {
  const now = new Date();
  const startDate = new Date(auction.startDateTime);
  const timeAuctionMs = parseAuctionDuration(auction.timeAuction);
  const endDate = new Date(startDate.getTime() + timeAuctionMs);
  if (now < startDate) return 'btn-warning';
  if (now >= startDate && now <= endDate) return 'btn-success';
  return 'btn-danger';
};

const buttonLabel = (auction: AuctionModel) => {
  const now = new Date();
  const startDate = new Date(auction.startDateTime);
  const timeAuctionMs = parseAuctionDuration(auction.timeAuction);
  const endDate = new Date(startDate.getTime() + timeAuctionMs);

  if (now < startDate) return 'Sắp tới';
  if (now >= startDate && now <= endDate) return 'Tham gia';
  return 'Hết hạn';
};

const parseAuctionDuration = (duration: string): number => {
  const [hours, minutes, seconds] = duration.split(':').map(Number);
  return ((hours * 60 * 60) + (minutes * 60) + seconds) * 1000;
};

  
      onMounted(() => {
        fetchAuctions();
      });
  
      return {
        auctions,
        loading,
        errorMessage,
        formatDate,
        getImageUrl,
        buttonClass,
        buttonLabel,
      };
    },
  });
  </script>
  
  <style scoped>
  .auction-list {
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 8px;
  }
  
  .auction-card {
    transition: all 0.3s ease;
    border-radius: 10px;
    padding: 10px;
    background-color: #fff;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
  }
  
  
  .auction-card:hover {
    transform: translateY(-5px);
    box-shadow: 0px 10px 20px rgba(0, 0, 0, 0.1);
  }
  
  .card-img-top {
    height: 200px;
    object-fit: cover;
    border-radius: 10px;
    margin: 10px 0;
    padding: 5px;
    background-color: #f0f0f0;
  }
  
  .btn {
    border-radius: 50px;
  }
  
  .alert-info, .alert-danger {
    text-align: center;
    margin-top: 20px;
  }
  </style>
  