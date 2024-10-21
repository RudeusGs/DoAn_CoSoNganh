<template>
  <div class="auction-list container mt-5 mb-5">
    <h2 class="text-center mb-4">Danh sách đấu giá</h2>
    
    <div v-if="loading" class="text-center">
      <p>Đang tải các phiên đấu giá...</p>
    </div>
    
    <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>
    
    <div v-if="auctions.length > 0" class="row g-4">
      <div class="col-md-6 col-lg-4" v-for="(auction, index) in auctions" :key="auction.id">
        <div class="auction-card card shadow-sm">
          <img 
            :src="getImageUrl(auction.image)" 
            class="card-img-top" 
            alt="Hình ảnh đấu giá" 
            @load="imageLoaded(index)"
            @error="handleImageError(index)"
            v-if="!imageError[index]"
          />
          <div v-else class="text-center">
            <img src="https://via.placeholder.com/336x198?text=No+Image" alt="No Image" />
          </div>
          <div class="card-body">
            <h5 class="card-title">{{ auction.auctionName }}</h5>
            <p v-if="isAuctionActive(auction)" class="card-text">Giá bắt đầu: {{ auction.startPrice }} VNĐ</p>
            <p v-else-if="!isAuctionActive(auction) && auction.currentPrice" class="card-text">Giá cuối cùng: {{ auction.currentPrice }} VNĐ</p>
            <p v-if="isAuctionActive(auction)" class="card-text">Ngày bắt đầu: {{ formatDate(auction.startDateTime) }}</p>
            <div v-if="!isAuctionActive(auction)">
              <p v-if="auction.winnerName" class="text-success"><strong>Người chiến thắng:</strong> {{ auction.winnerName }}</p>
              <p v-else class="text-danger"><strong>Không có người chiến thắng trong phiên đấu giá này.</strong></p>
            </div>
            
            <div v-if="isAuctionActive(auction)">
              <router-link :to="{ name: 'auctionDetail', params: { id: auction.id } }">
                <button :class="buttonClass(auction)" class="btn w-100 mt-3">
                  {{ buttonLabel(auction) }}
                </button>
              </router-link>
            </div>
            <div v-else>
              <button :class="buttonClass(auction)" class="btn w-100 mt-3" disabled>
                {{ buttonLabel(auction) }}
                <span v-if="buttonLabel(auction) !== 'Tham gia'">
                  <i class="fas fa-info-circle ms-2" title="Không thể tham gia đấu giá này"></i>
                </span>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>     
    <div v-else class="alert alert-info">Hiện tại không có phiên đấu giá nào.</div>
  </div>
</template>



<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import AuctionApi from '@/api/auction.api';
import UserApi from '@/api/user.api'; // Import UserApi
import type { AuctionModel } from '@/models/auction-model';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'AuctionList',
  setup() {
    const auctions = ref<AuctionModel[]>([]);
    const loading = ref(true);
    const errorMessage = ref<string | null>(null);
    const router = useRouter();

    // Cache để lưu trữ tên người dùng đã lấy
    const usersCache = ref<Record<number, string>>({});

    // Phương thức lấy tên người dùng dựa trên userId
    const fetchUserName = async (userId: number): Promise<string> => {
      if (usersCache.value[userId]) {
        return usersCache.value[userId];
      }
      try {
        const response = await UserApi.getUserById(userId);
        if (response.data.result && response.data.result.data) {
          const userName = response.data.result.data.fullName || 'Người dùng';
          usersCache.value[userId] = userName;
          return userName;
        }
      } catch (error) {
        console.error(`Không thể lấy thông tin người dùng với ID ${userId}:`, error);
      }
      return 'Không xác định';
    };

    // Phương thức kiểm tra phiên đấu giá có đang hoạt động không
    const isAuctionActive = (auction: AuctionModel): boolean => {
      const now = new Date();
      const startDate = new Date(auction.startDateTime);
      const durationMs = parseAuctionDuration(auction.timeAuction);
      const endDate = new Date(startDate.getTime() + durationMs);
      return now >= startDate && now <= endDate;
    };

    // Phương thức kiểm tra phiên đấu giá có sắp tới không
    const isAuctionUpcoming = (auction: AuctionModel): boolean => {
      const now = new Date();
      const startDate = new Date(auction.startDateTime);
      return now < startDate;
    };

    // Phương thức định dạng lớp CSS cho nút
    const buttonClass = (auction: AuctionModel) => {
      if (isAuctionActive(auction)) return 'btn-success';
      if (isAuctionUpcoming(auction)) return 'btn-warning';
      return 'btn-danger';
    };

    // Phương thức định dạng nhãn cho nút
    const buttonLabel = (auction: AuctionModel) => {
      if (isAuctionActive(auction)) return 'Tham gia';
      if (isAuctionUpcoming(auction)) return 'Sắp tới';
      return 'Hết hạn';
    };

    // Phương thức chuyển đổi duration thành milliseconds
    const parseAuctionDuration = (duration: string): number => {
      const [hours, minutes, seconds] = duration.split(':').map(Number);
      return ((hours * 60 * 60) + (minutes * 60) + seconds) * 1000; // Convert to milliseconds
    };

    // Phương thức lấy URL đầy đủ cho hình ảnh
    const getImageUrl = (imagePath: string) => {
      const baseUrl = 'https://localhost:7224/';
      return `${baseUrl}${imagePath}`;
    };

    // Phương thức định dạng ngày tháng
    const formatDate = (dateString: string) => {
      const date = new Date(dateString);
      return date.toLocaleDateString('vi-VN', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      });
    };

    // Phương thức fetch auctions và thêm thông tin người chiến thắng
    const fetchAuctions = async () => {
      try {
        const response = await AuctionApi.getAllAuction();
        if (response.data.result && response.data.result.data.length > 0) {
          const fetchedAuctions = response.data.result.data as AuctionModel[];
          
          // Lấy thông tin người chiến thắng cho các phiên đấu giá đã kết thúc
          const auctionsWithWinners = await Promise.all(fetchedAuctions.map(async (auction) => {
            if (!isAuctionActive(auction) && auction.winner) {
              auction.winnerName = await fetchUserName(auction.winner);
            }
            return auction;
          }));
          
          auctions.value = auctionsWithWinners;
        } else {
          errorMessage.value = 'Không tìm thấy phiên đấu giá nào.';
        }
      } catch (error) {
        console.error('Lỗi khi tải dữ liệu phiên đấu giá:', error);
        errorMessage.value = 'Không thể tải dữ liệu phiên đấu giá.';
      } finally {
        loading.value = false;
      }
    };

    // Các phương thức xử lý trạng thái hình ảnh
    const imageError = ref<boolean[]>([]);

    const imageLoaded = (index: number) => {
      // Bạn có thể thêm logic xử lý khi hình ảnh đã tải thành công
      // Ví dụ: Ẩn loader nếu có
    };

    const handleImageError = (index: number) => {
      imageError.value[index] = true;
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
      isAuctionActive,
      isAuctionUpcoming,
      imageError,
      imageLoaded,
      handleImageError,
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
    min-height: 464px;
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
  