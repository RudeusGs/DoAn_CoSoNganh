<template>
    <div class="container mt-5">
      <h2 class="text-center">{{ accountDetails?.name }} - Chi tiết tài khoản</h2>
      <div class="row mt-4">
        <div class="col-md-6">
          <div class="card">
            <img 
              :src="getFullImageUrl(accountDetails?.image)" 
              class="card-img-top" 
              alt="Account Image" 
            />
            <div class="card-body">
              <h5 class="card-title">Thông tin tài khoản</h5>
              <p class="card-text"><strong>Server:</strong> {{ accountDetails?.server }}</p>
              <p class="card-text"><strong>Cấp VIP:</strong> {{ accountDetails?.vipLevel }}</p>
              <p class="card-text"><strong>Giá:</strong> {{ accountDetails?.price }} VNĐ</p>
              <p class="card-text"><strong>Giá gốc:</strong> {{ accountDetails?.originalPrice }} VNĐ</p>
              <p class="card-text"><strong>Khuyến mãi:</strong> {{ accountDetails?.discount }}%</p>
              <p class="card-text"><strong>Thông tin thêm:</strong> {{ accountDetails?.additionalInfo }}</p>
              <a href="#" class="btn btn-primary">Mua ngay</a>
            </div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="card">
            <div class="card-body">
              <h5 class="card-title">Hình ảnh chi tiết</h5>
              <img 
                :src="getFullImageUrl(accountDetails?.detailedImages[0])" 
                class="img-fluid" 
                alt="Detailed Images"
              />
              <!-- You can implement a carousel here for detailed images -->
            </div>
          </div>
        </div>
      </div>
      <div class="mt-4">
        <h4>Chi tiết dịch vụ</h4>
        <p>{{ accountDetails?.serviceDetails }}</p>
      </div>
    </div>
  </template>
  
  <script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import homeApi from '@/api/home.api';
import { useRoute } from 'vue-router';

export default defineComponent({
  name: 'GameAccountDetail',
  setup() {
    const accountDetails = ref<any>(null);
    const route = useRoute();
    const id = Number(route.params.id); // Get ID from route parameters

    const fetchAccountDetails = async (id: number) => {
      try {
        const response = await homeApi.getById(id);
        console.log(response);
        if (response && response.data.result.isSuccess) {
          accountDetails.value = response.data.result.data;
        } else {
          console.error("Error fetching account details:", response.data.result.message);
        }
      } catch (error) {
        console.error("Error fetching account details:", error);
      }
    };

    onMounted(() => {
      console.log("Component mounted with ID:", id);
      fetchAccountDetails(id);
    });

    const getFullImageUrl = (imageString: string) => {
      const baseUrl = 'https://localhost:7224/';
      return `${baseUrl}${imageString}`;
    };

    return {
      accountDetails,
      getFullImageUrl,
    };
  },
});
</script>

  
  <style scoped>
  .container {
    max-width: 1200px;
    margin: 0 auto;
  }
  
  .card {
    margin-bottom: 20px;
  }
  </style>
  