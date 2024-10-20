<template>
  <div class="auction-container container mt-4 mb-5">
    <div class="row">
      <!-- Cột trái: Thông tin người dùng -->
      <div class="col-md-3">
        <div class="card shadow-lg mb-4">
          <div class="card-body">
            <h5 class="card-title">Tài khoản của bạn</h5>
            <p class="card-text"><strong>Tên đăng nhập:</strong> {{ user.username }}</p>
            <p class="card-text"><strong>Số dư:</strong> {{ user.balance }} VNĐ</p>
          </div>
        </div>
      </div>

      <!-- Cột giữa: Thông tin đấu giá -->
      <div class="col-md-6" v-if="auction">
        <div class="card shadow-lg text-center">
          <h5 class="card-title mt-4">{{ auction.auctionName }}</h5>
          <div class="image-container">
            <img :src="getImageUrl(auction.image)" class="auction-image img-fluid rounded" alt="Hình ảnh đấu giá">
          </div>
          <div class="card-body">        
            <p class="card-text">{{ auction.prize }}</p>
             <div class="current-price-box">
            <p class="card-text">
              <strong>Giá hiện tại:</strong> {{ auction.currentPrice }} VNĐ
            </p>
          </div>
            <p class="card-text"><strong>Thời gian đấu giá:</strong> {{ auction.timeAuction }}</p>
          </div>
        </div>

        <!-- Phần đặt giá, cách 20px so với thông tin đấu giá -->
        <div class="card shadow-lg mt-4" style="margin-top: 20px;">
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

      <!-- Cột phải: Thông tin người tham gia -->
      <div class="col-md-3">
        <div class="card shadow-lg mb-4">
          <div class="card-body">
            <h5 class="card-title">Người tham gia</h5>
            <ul class="list-group">
              <li class="list-group-item" v-for="participant in participants" :key="participant.id">
                {{ participant.name }}
              </li>
            </ul>
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
    const participants = ref([
      { id: 1, name: 'Nguyễn Văn A' },
      { id: 2, name: 'Trần Thị B' },
      // Thêm người tham gia nếu cần
    ]);
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
      participants,
    };
  },
});
</script>
<style scoped>
.auction-container {
  padding: 20px;
}

.auction-image {
  width: 100%;
  max-width: 300px;
  height: auto;
  object-fit: cover;
  margin: 0 auto;
}

.image-container {
  text-align: center;
  padding: 20px 0;
}

.card-title {
  margin-bottom: 10px;
}

.card-text {
  margin-bottom: 8px;
}

/* Điều chỉnh khoảng cách giữa các card */
.card + .card {
  margin-top: 20px;
}

/* Đảm bảo cột giữa được căn giữa trên màn hình nhỏ */
@media (max-width: 768px) {
  .col-md-6 {
    margin-left: auto;
    margin-right: auto;
  }
}
</style>
