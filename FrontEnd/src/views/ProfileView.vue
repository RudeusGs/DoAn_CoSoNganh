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
      </div>
    </section>
  </template>
  
  <script setup lang="ts">
  import { ref, onMounted } from "vue";
  import { storeToRefs } from "pinia";
  import { userStore } from "@/stores/auth";
  
  const authStore = userStore();
  const { user } = storeToRefs(authStore);
  
  const userModel = ref({
      id: '',
      userName: '',
      fullName: '',
      email: '',
      balance: '', 
      coin: 0,    
  })
  
  const init = () => {
      if (user.value) {
          userModel.value.email = user.value.email
          userModel.value.userName = user.value.userName
          userModel.value.fullName = user.value.fullName
          userModel.value.id = user.value.id
          userModel.value.balance = user.value.balance
          userModel.value.coin = user.value.coin
      }
  }

  onMounted(() => {
      init()
  })
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
  
  .title {
    text-align: center;
    margin-bottom: 40px;
    font-size: 2em;
    color: #333;
  }
  
  .form-group label {
    font-weight: bold;
    color: #555;
  }
  
  .form-control {
    border-radius: 5px;
  }
  
  .btn-outline-secondary {
    border-color: #7f7f7f;
    color: #7f7f7f;
  }
  
  .btn-outline-secondary:hover {
    background-color: #7f7f7f;
    color: #fff;
  }
  
  .profile-stats .stat-item {
    margin-bottom: 15px;
  }
  
  .profile-stats .stat-item h3 {
    font-size: 1.5em;
    color: #333;
  }
  
  .profile-stats .stat-item p {
    color: #777;
    font-size: 1em;
  }
  
  @media (max-width: 768px) {
    .form-group.row {
      flex-direction: column;
      align-items: flex-start;
    }
  
    .form-group.row label {
      margin-bottom: 5px;
    }
  
    .form-group.row .col-sm-9 {
      width: 100%;
    }
  }
  </style>
  