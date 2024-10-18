<template>
    <div class="auction-container container mt-5 mb-5">
      <div class="row">
        <div class="col-md-8" v-if="auction">
          <div class="card shadow-lg">
            <div class="image-container">
                <img :src="getImageUrl(auction.image)" class="auction-image" alt="Hình ảnh đấu giá">
              </div>
              
            <div class="card-body">
              <h5 class="card-title">{{ auction.auctionName }}</h5>
              <p class="card-text">{{ auction.prize }}</p>
              <p class="card-text"><strong>Giá hiện tại:</strong> {{ auction.currentPrice }} VNĐ</p>
              <p class="card-text"><strong>Giá bắt đầu:</strong> {{ auction.startPrice }} VNĐ</p>
              <p class="card-text"><strong>Ngày bắt đầu:</strong> {{ formatDate(auction.startDateTime) }}</p>
              <p class="card-text"><strong>Thời gian đấu giá:</strong> {{ auction.timeAuction }}</p>
              <p class="card-text"><strong>Trạng thái:</strong> {{ auction.status }}</p>
            </div>
          </div>
        </div>
  
        <div class="col-md-4">
          <div class="card shadow-lg mb-4">
            <div class="card-body">
              <h5 class="card-title">Tài khoản của bạn</h5>
              <p class="card-text"><strong>Tên đăng nhập:</strong> {{ user.username }}</p>
              <p class="card-text"><strong>Số dư:</strong> {{ user.balance }} VNĐ</p>
            </div>
          </div>
  
          <div class="card shadow-lg">
            <div class="card-body">
              <h5 class="card-title">Đặt giá của bạn</h5>
              <form @submit.prevent="placeBid">
                <div class="mb-3">
                  <label for="bidAmount" class="form-label">Số tiền đặt giá (VNĐ)</label>
                  <input type="number" v-model="bidAmount" id="bidAmount" class="form-control" required>
                </div>
                <button type="submit" class="btn btn-primary w-100">Đặt giá</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  
  
  <script lang="ts">
import { defineComponent, ref, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';
import AuctionApi from '@/api/auction.api';
import type { AuctionModel } from '@/models/auction-model';

export default defineComponent({
  setup() {
    const route = useRoute();
    const auction = ref<AuctionModel | null>(null);
    const bidAmount = ref(0);
    const user = ref({
      username: 'john_doe',
      balance: 1000000,
    });

    const auctionId = ref(Number(route.params.id));

    const formatDate = (dateString: string) => {
      const date = new Date(dateString);
      return date.toLocaleString('vi-VN', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
      });
    };

    const fetchAuction = async () => {
  try {
    const response = await AuctionApi.getByIdAuction(auctionId.value);
    if (response.data.result && response.data.result.data) {
      auction.value = response.data.result.data as AuctionModel;
    } else {
      console.error('Không tìm thấy phiên đấu giá');
    }
  } catch (error) {
    console.error('Lỗi khi lấy chi tiết phiên đấu giá:', error);
  }
};


    watch(() => route.params.id, (newId) => {
      auctionId.value = Number(newId);
      fetchAuction();
    });

    onMounted(() => {
      fetchAuction();
    });

    const placeBid = async () => {
  if (!auction.value) return;

  const currentPrice = Number(auction.value.currentPrice);
  if (bidAmount.value > currentPrice) {
    const updatedPriceModel = {
      id: auction.value.id,
      currentPrice: bidAmount.value.toString(),
    //   winner: user.value.id, // Giả sử bạn có thông tin người dùng
    };
    try {
      const response = await AuctionApi.updateCurrentPriceAuction(updatedPriceModel);
      if (response.data && response.data.isSuccess) {
        alert(`Bạn đã đặt giá ${bidAmount.value} VNĐ`);
        auction.value.currentPrice = bidAmount.value.toString(); // Cập nhật giá hiện tại
      } else {
        alert(response.data.message || 'Có lỗi xảy ra khi đặt giá.');
      }
    } catch (error) {
      console.error('Lỗi khi đặt giá:', error);
    }
  } else {
    alert('Giá đặt phải cao hơn giá hiện tại!');
  }
};


const getImageUrl = (imagePath: string) => {
  const baseUrl = 'https://localhost:7224/';
  return `${baseUrl}${imagePath}`;
};


    onMounted(() => {
      fetchAuction();
    });

    return {
      auction,
      user,
      bidAmount,
      formatDate,
      placeBid,
      getImageUrl,
    };
  },
});
</script>
<style scoped>
.auction-container {
  padding: 20px;
}

.custom-col {
  width: 848px;
  max-width: 100%;
  height: 518px;
  margin: 0 auto;
}

.auction-card {
  height: 100%;
}

.image-container {
  height: 60%;
  overflow: hidden;
}

.auction-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.card-body {
  height: 40%;
  overflow: auto;
  padding: 10px;
}

.card-title {
  margin-bottom: 10px;
}

.card-text {
  margin-bottom: 8px;
}

/* Các CSS khác nếu cần */
</style>


