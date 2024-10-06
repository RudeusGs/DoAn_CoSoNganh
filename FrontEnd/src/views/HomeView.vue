<template>
  <div class="container">     
    <div class="row mt-5 g-2 justify-content-center">
      <h2 class="d-flex justify-content-center">Các tài khoản gần đây</h2>
      <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>
      <div class="preview-card col-6 col-sm-6 col-md-4 mb-4" v-for="(item, index) in gameAccount" :key="index">
        <div class="preview-card__wrp">
          <div class="preview-card__item">
            <div class="preview-card__img">
              <img :src="item.image || 'https://via.placeholder.com/150'" :alt="item.title" @load="imageLoaded(index)" @error="handleImageError(index)" />
              <div v-if="loadingImages[index]">Loading image...</div>
              <div v-if="!loadingImages[index] && imageError[index]" class="alert alert-danger">Image failed to load.</div>
            </div>
            
            <div class="preview-card__content">
              <span class="preview-card__code">{{ item.price }} VNĐ</span>
              <div class="preview-card__title">Hành tinh: {{ item.planet }}</div> 
              <div class="preview-card__text">Server: {{ item.server }}</div>
              <div class="d-flex justify-content-between">
                <a href="#" class="preview-card__button">Read More</a>
                <a href="#" class="preview-card__button">Buy</a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import homeApi from "@/api/home.api";

export default defineComponent({
  name: 'GameAccountComponent',
  setup() {
    const gameAccount = ref<any>([]);
    const errorMessage = ref<string | null>(null);
    const loadingImages = ref<boolean[]>([]);
    const imageError = ref<boolean[]>([]);

    const fetchData = async () => {
      try {
        const response = await homeApi.getAllGameAccounts();
        if (response && response.data.result.isSuccess) {
          gameAccount.value = response.data.result.data;
          loadingImages.value = new Array(gameAccount.value.length).fill(true);
          imageError.value = new Array(gameAccount.value.length).fill(false);

          console.log(gameAccount.value);
          gameAccount.value.forEach((item: { image: any; }) => {
            console.log('Image URL:', item.image);
          });
          errorMessage.value = null;
        } else {
          gameAccount.value = [];
          errorMessage.value = response?.data.result.message ?? "Lỗi";
        }
      } catch (error) {
        gameAccount.value = [];
        errorMessage.value = "Có lỗi xảy ra khi tải dữ liệu.";
      }
    };

    const imageLoaded = (index: number) => {
      loadingImages.value[index] = false;
    };

    const handleImageError = (index: number) => {
      loadingImages.value[index] = false;
      imageError.value[index] = true;
    };

    onMounted(fetchData);

    return {
      gameAccount,
      errorMessage,
      loadingImages,
      imageLoaded,
      handleImageError,
      imageError,
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

.preview-card__item {
  display: flex;
  flex-direction: column; 
}

.preview-card__img {
  border-radius: 15px 15px 0 0;
  margin: 10px;
}

.preview-card__img img {
  width: 100%;
  height: auto;
  border-radius: 15px 15px 0 0;
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
}
</style>
