<template>
  <div class="container">     
    <div class="row mt-5 g-2 justify-content-center">
      <div class="filter-controls mb-3 p-4 bg-light rounded shadow-sm">
        <div class="row">
          <div class="col-md-4 mb-3">
            <label for="filterServer" class="form-label">Chọn Server</label>
            <select v-model="filterServer" id="filterServer" class="form-select">
              <option value="">Chọn Server</option>
              <option v-for="server in uniqueServers" :key="server" :value="server">{{ server }}</option>
            </select>
          </div>

          <div class="col-md-4 mb-3">
            <label for="filterPrice" class="form-label">Chọn Giá tối đa</label>
            <select v-model="filterPrice" id="filterPrice" class="form-select">
              <option value="">Chọn Giá tối đa</option>
              <option v-for="price in priceOptions" :key="price" :value="price">{{ price }} VNĐ</option>
            </select>
          </div>

          <div class="col-md-4 mb-3">
            <label for="filterPlanet" class="form-label">Chọn Hành tinh</label>
            <select v-model="filterPlanet" id="filterPlanet" class="form-select">
              <option value="">Chọn Hành tinh</option>
              <option v-for="planet in uniquePlanets" :key="planet" :value="planet">{{ planet }}</option>
            </select>
          </div>
        </div>
      </div>

      <h2 class="d-flex justify-content-center">Các tài khoản gần đây</h2>
      <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>

      <div 
        class="preview-card col-6 col-sm-6 col-md-4 mb-4" 
        v-for="(item, index) in filteredAccounts" 
        :key="index"
      >
        <div class="preview-card__wrp">
          <div class="preview-card__item">
            <div class="preview-card__header">
              <img 
                class="avatar" 
                src="https://via.placeholder.com/40" 
                alt="Avatar"
              />
              <div class="poster-info">
                <span class="poster-name">{{ item.name }}</span>
                <span class="post-time">{{ timeSince(item.createdDate) }}</span>
              </div>
            </div>

            <div class="preview-card__img">
              <div class="image-border-wrapper">
                <img 
                  class="card-img-top" 
                  :src="getFullImageUrl(item.image)" 
                  alt="Product image" 
                  @load="imageLoaded(index)"
                  @error="handleImageError(index)"
                   @click="openImageModal(getFullImageUrl(item.image), item.image.split(';'))"
                  style="width: 336px; height: 198px; object-fit: cover;"
                />
              </div>
            </div>
            
            <div class="preview-card__content">
              <span class="preview-card__code">{{ item.price }} VNĐ</span>
              <div class="preview-card__title">Hành tinh: {{ item.planet }}</div> 
              <div class="preview-card__text">Server: {{ item.server }}</div>
              <div class="d-flex justify-content-between">
                <a href="#" class="preview-card__button">Like</a>
                <a href="#" class="preview-card__button">Comment</a>
                <a href="#" class="preview-card__button" data-bs-toggle="modal" data-bs-target="#confirmationModal" @click="selectedAccount = item">Buy</a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <div class="modal fade" id="imageModal" tabindex="-1" aria-labelledby="imageModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="imageModalLabel">Xem Ảnh Phóng To</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body text-center">
            <img :src="selectedImage" class="img-fluid" alt="Enlarged Image" />
          </div>
          <div class="modal-footer">
            <button class="btn btn-secondary" @click="showPrevImage" :disabled="currentImageIndex === 0">Prev</button>
            <button class="btn btn-secondary" @click="showNextImage" :disabled="currentImageIndex === imageList.length - 1">Next</button>
          </div>
        </div>
      </div>
    </div>
    

    <div class="modal fade" id="confirmationModal" tabindex="-1" aria-labelledby="confirmationModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="confirmationModalLabel">Xác nhận mua hàng</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <p>{{ senderId }} muốn mua tài khoản <strong>{{ selectedAccount?.name }}</strong> của bạn, bạn có đồng ý không?</p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Không</button>
            <button type="button" class="btn btn-primary" @click="confirmPurchase">Có</button>
          </div>
        </div>
      </div>
    </div>
    
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, onBeforeUnmount, computed } from 'vue';
import homeApi from "@/api/home.api";
declare const bootstrap: any;
interface GameAccount {
  name: string;
  accountName: string;
  accountPassword: string;
  image: string;
  price: number;
  status: string;
  earring: string;
  planet: string;
  server: string;
  createdDate: string;
  id: number;
  created: string;
  updatedDate: string;
  deleteDate?: string;
  timeAgo?: string;
}

export default defineComponent({
  name: 'GameAccountComponent',
  setup() {
    const gameAccount = ref<GameAccount[]>([]);
    const errorMessage = ref<string | null>(null);
    const loadingImages = ref<boolean[]>([]);
    const imageError = ref<boolean[]>([]);
    const filterServer = ref<string>('');
    const filterPrice = ref<number | null>(null);
    const filterPlanet = ref<string>('');
    const uniqueServers = ref<string[]>([]);
    const uniquePlanets = ref<string[]>([]);
    const priceOptions = ref<number[]>([100000, 400000, 700000, 1000000, 3000000]);
    const selectedAccount = ref<GameAccount | null>(null);
      const selectedImage = ref<string | undefined>(undefined);
        const imageList = ref<string[]>([]);
          const currentImageIndex = ref<number>(0);
            const senderId = ref<string>('SenderId');

    let timer: ReturnType<typeof setInterval>;
      
      const openImageModal = (imageUrl: string, images: string[]) => {
    selectedImage.value = imageUrl;
    imageList.value = images;
    currentImageIndex.value = images.indexOf(imageUrl); // Set chỉ số hình ảnh hiện tại

    const modalElement = document.getElementById('imageModal');
    if (modalElement) {
      const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
      modal.show();
    }
  };
  const showNextImage = () => {
  if (currentImageIndex.value < imageList.value.length - 1) {
    currentImageIndex.value++;
    selectedImage.value = imageList.value[currentImageIndex.value];
    console.log('Next Image URL:', selectedImage.value);
  } else {
    console.log('No more images to show.');
  }
};

const showPrevImage = () => {
  if (currentImageIndex.value > 0) {
    currentImageIndex.value--;
    selectedImage.value = imageList.value[currentImageIndex.value];
    console.log('Previous Image URL:', selectedImage.value);
  } else {
    console.log('No previous images to show.');
  }
};


    const fetchData = async () => {
      try {
        const response = await homeApi.getAllGameAccounts();
        if (response && response.data.result.isSuccess) {
          gameAccount.value = response.data.result.data;
          loadingImages.value = new Array(gameAccount.value.length).fill(true);
          imageError.value = new Array(gameAccount.value.length).fill(false);
          errorMessage.value = null;
          uniqueServers.value = [...new Set(gameAccount.value.map(item => item.server))];
          uniquePlanets.value = [...new Set(gameAccount.value.map(item => item.planet))];
        } else {
          gameAccount.value = [];
          errorMessage.value = response?.data.result.message ?? "Lỗi";
        }
      } catch (error) {
        gameAccount.value = [];
        errorMessage.value = "Có lỗi xảy ra khi tải dữ liệu.";
      }
    };

    const getFullImageUrl = (imageString: string) => {
      const baseUrl = 'https://localhost:7224/';
      const firstImage = imageString.split(';')[0];
      return `${baseUrl}${firstImage}`;
    };

    const imageLoaded = (index: number) => {
      loadingImages.value[index] = false;
    };

    const handleImageError = (index: number) => {
      loadingImages.value[index] = false;
      imageError.value[index] = true;
    };

    const timeSince = (dateString: string) => {
      const date = new Date(dateString);
      const seconds = Math.floor((new Date().getTime() - date.getTime()) / 1000);
      let interval = Math.floor(seconds / 31536000);
      if (interval > 1) return `${interval} years ago`;
      interval = Math.floor(seconds / 2592000);
      if (interval > 1) return `${interval} months ago`;
      interval = Math.floor(seconds / 86400);
      if (interval > 1) return `${interval} days ago`;
      interval = Math.floor(seconds / 3600);
      if (interval > 1) return `${interval} hours ago`;
      interval = Math.floor(seconds / 60);
      if (interval > 1) return `${interval} minutes ago`;
      return "just now";
    };

    const updateTimestamps = () => {
      gameAccount.value.forEach((item) => {
        item.timeAgo = timeSince(item.createdDate);
      });
    };

    const confirmPurchase = () => {
  if (selectedAccount.value) {
    alert(`Bạn đã xác nhận mua tài khoản: ${selectedAccount.value.name}`);
    
    // Add the logic to handle the purchase here, such as sending a request to your server.

    const modalElement = document.getElementById('confirmationModal');
    if (modalElement) {
      const modal = bootstrap.Modal.getInstance(modalElement);
      modal.hide();
    }
  }
};


    const filteredAccounts = computed(() => {
      return gameAccount.value.filter(item => {
        const matchesServer = filterServer.value ? item.server === filterServer.value : true;
        const matchesPrice = filterPrice.value ? item.price <= filterPrice.value : true;
        const matchesPlanet = filterPlanet.value ? item.planet === filterPlanet.value : true;
        return matchesServer && matchesPrice && matchesPlanet;
      });
    });

    onMounted(() => {
      fetchData();
      timer = setInterval(updateTimestamps, 60000);
    });

    onBeforeUnmount(() => {
      clearInterval(timer);
    });

    return {
      gameAccount,
      errorMessage,
      loadingImages,
      imageError,
      filterServer,
      filterPrice,
      filterPlanet,
      uniqueServers,
      uniquePlanets,
      priceOptions,
      selectedAccount,
      confirmPurchase,
      filteredAccounts,
      getFullImageUrl,
      timeSince,
      imageLoaded,
      handleImageError,
      selectedImage,
      openImageModal,
    showNextImage,
    showPrevImage,
    imageList,
    currentImageIndex,
    };
  },
});
</script>


<style scoped>
@import url("https://fonts.googleapis.com/css?family=Fira+Sans:400,500,600,700,800");
* {
  box-sizing: border-box;
}

body {
  background-color: #f5f5f5; 
  min-height: 100vh;
  font-family: "Fira Sans", sans-serif;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
}


.preview-card {
  position: relative;
  background: #fff;
  box-shadow: 0 2px 15px rgba(0, 0, 0, 0.1);
  border-radius: 15px; 
  overflow: hidden; 
  transition: box-shadow 0.3s;
  margin: 10px;
}

.preview-card__wrp {
  padding: 10px;
}

.preview-card__item {
  display: flex;
  flex-direction: column; 
}
.image-border-wrapper {
  border: 2px solid #ddd; /* Change the color as needed */
  border-radius: 8px; /* Optional: Add rounded corners */
  padding: 5px; /* Space between the image and the border */
  margin: auto; /* Center the border wrapper */
}

.preview-card__header {
  display: flex;
  align-items: center;
  padding: 10px;
  border-bottom: 1px solid #ddd;
}

.avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  object-fit: cover;
  margin-right: 10px;
}

.poster-info {
  display: flex;
  flex-direction: column;
}

.poster-name {
  font-weight: 600;
  color: #333;
}

.post-time {
  font-size: 12px;
  color: #777;
}

.preview-card__img {
  margin: 10px;
  position: relative;
}

.preview-card__img img {
  width: 100%;
  height: auto;
}

.preview-card__content {
  padding: 20px;
}

.preview-card__code {
  color: #aaa; 
  margin-bottom: 10px;
}

.preview-card__title {
  font-size: 20px;
  font-weight: 700;
  color: #333; 
  margin-bottom: 10px;
}

.preview-card__text {
  color: #555; 
  margin-bottom: 20px;
}

.preview-card__button {
  display: inline-block;
  padding: 10px 20px;
  border-radius: 50px; 
  background-color: #7f7f7f; 
  color: #fff; 
  text-decoration: none;
  transition: background-color 0.3s;
}

.preview-card__button:hover {
  background-color: #454545; 
}

@media (max-width: 576px) {
  .preview-card {
    margin: 5px;
  }

  .preview-card__header {
    padding: 5px;
  }

  .poster-name {
    font-size: 14px;
  }

  .post-time {
    font-size: 10px;
  }
}
.filter-controls {
  background-color: #f8f9fa;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.form-label {
  font-weight: bold;
  color: #495057;
}

.form-select {
  border: 2px solid #ced4da;
  border-radius: 4px;
  padding: 10px;
}

.mb-3 {
  margin-bottom: 1rem !important;
}

.p-4 {
  padding: 1.5rem !important;
}

.shadow-sm {
  box-shadow: 0 .125rem .25rem rgba(0, 0, 0, .075) !important;
}
</style>

