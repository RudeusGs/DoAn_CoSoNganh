<template>
  <section class="user-info-section py-5">
    <div class="container px-4 px-lg-5">
      <!-- Section Title -->
      <div class="row d-flex justify-content-center text-center mb-5">
        <h2 class="fw-bolder col-md-8">Thông tin cá nhân</h2>
      </div>

      <!-- User Information Form -->
      <div class="row d-flex justify-content-center mb-4">
        <form class="col-md-8 bg-light p-4 rounded shadow">

          <!-- Họ và tên (Full Name) -->
          <div class="form-group row mb-3">
            <label class="col-sm-3 col-form-label">Họ và tên</label>
            <div class="col-sm-9">
              <input type="text" class="form-control" v-model="userModel.fullName" disabled placeholder="Họ và tên">
            </div>
          </div>

          <!-- Email -->
          <div class="form-group row mb-3">
            <label class="col-sm-3 col-form-label">Email</label>
            <div class="col-sm-9">
              <input type="email" class="form-control" v-model="userModel.email" disabled placeholder="Email">
            </div>
          </div>

          <!-- Số dư (Balance) -->
          <div class="form-group row mb-3">
            <label class="col-sm-3 col-form-label">Số dư</label>
            <div class="col-sm-9">
              <input type="text" class="form-control bg-light" v-model="userModel.balance" disabled>
            </div>
          </div>

          <!-- Coins -->
          <div class="form-group row mb-3">
            <label class="col-sm-3 col-form-label">Coins</label>
            <div class="col-sm-9">
              <input type="number" class="form-control bg-light" v-model="userModel.coin" disabled>
            </div>
          </div>
        </form>
      </div>

      <!-- User Posts Section -->
      <div class="row d-flex justify-content-center text-center mb-5">
        <h2 class="fw-bolder col-md-8">Tài khoản đã đăng</h2>
      </div>

      <!-- Posts List -->
      <div v-if="userPosts.length" class="row justify-content-center">
        <div class="col-md-10">
          <div v-for="(post, index) in userPosts" :key="index" class="card mb-3">
            <div class="card-body">
              <p class="card-text">Tài khoản: 
                <input type="text" class="form-control" v-model="post.accountName" disabled />
              </p>
              <p class="card-text">Mật khẩu: 
                <input type="password" class="form-control" v-model="post.accountPassword" disabled />
              </p>               
              <p class="card-text">Nội dung: {{ post.content }}</p> 
              <p class="card-text">Server: {{ post.server }}</p> 
              <p class="card-text">Hành tinh: {{ post.planet }}</p>
              <p class="card-text">Giá: {{ post.price }} VNĐ</p>
              <p class="card-text">
                Trạng thái:
                <span :class="{ 'text-danger': post.status === true, 'text-success': post.status === false }">
                  {{ post.status === true ? 'Đã bán' : 'Chưa bán' }}
                </span>
              </p>
              <p class="card-text"><small class="text-muted">Đăng {{ timeSince(post.createdDate) }}</small></p>
            </div>
          </div>
        </div>
      </div>      
      <div v-else class="text-center">Không có tài khoản nào được đăng.</div>
    </div>
  </section>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import profile from '@/api/profile.api';
import { userStore } from '@/stores/auth';
import type { AddGameAccountModel } from '@/models/gameaccount-model';

const userPosts = ref<AddGameAccountModel[]>([]);

const userModel = ref({
  fullName: '',
  email: '',
  balance: 0,
  coin: 0,
});

// Get the logged-in user's ID from the store or local storage
const store = userStore();
const userId = store.user?.id || JSON.parse(localStorage.getItem('user') || '{}').id;

// Fetch the user profile data
const fetchUserProfile = async () => {
  try {
    if (userId) {
      const response = await profile.getByIdProfile(userId);
      if (response && response.data && response.data.result.isSuccess) {
        const userData = response.data.result.data;
        userModel.value = {
          fullName: userData.fullName || '',
          email: userData.email || '',
          balance: userData.balance || 0,
          coin: userData.coin || 0,
        };
      } else {
        console.error('Failed to fetch user profile:', response?.data?.result?.message);
      }
    } else {
      console.error('User ID not found.');
    }
  } catch (error) {
    console.error('Error fetching user profile:', error);
  }
};

// Fetch the user's posts (game accounts or whatever content they post)
const fetchUserPosts = async () => {
  try {
    if (userId) {
      const response = await profile.getGameAccountsByUser(userId);
      console.log('API response:', response); // Logging the response
      
      if (response && response.data && response.data.isSuccess) {
        userPosts.value = response.data.data as AddGameAccountModel[];
        console.log('User posts:', userPosts.value); // Log posts to verify
      } else {
        console.error('Failed to fetch user posts:', response?.data?.message);
      }
    } else {
      console.error('User ID not found.');
    }
  } catch (error) {
    console.error('Error fetching user posts:', error);
  }
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

// Call the functions when the component is mounted
onMounted(() => {
  fetchUserProfile();
  fetchUserPosts();  // Fetch user posts
});
</script>


<style scoped>
@import url("https://fonts.googleapis.com/css?family=Fira+Sans:400,500,600,700,800");

.user-info-section {
  background-color: #f5f5f5;
  font-family: "Fira Sans", sans-serif;
}

.container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}

.form-group label {
  font-weight: bold;
  color: #555;
}

.form-control {
  border-radius: 5px;
}

.card {
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.card-body {
  padding: 20px;
}

.card-title {
  font-weight: bold;
}

.card-text {
  font-size: 16px;
}
</style>
