<template>
  <nav class="navbar navbar-expand-md bg-body-tertiary">
    <div class="container-xl">
      <a class="navbar-brand" href="/">
        <img src="/public/LogoWebsite.png" alt="Logo">
      </a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="/">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="/about">About</a>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Payin</a>
            <ul class="dropdown-menu">
              <li><a class="dropdown-item" href="/phonecard">Phone Card</a></li>
              <li><a class="dropdown-item" href="/banking">Banking</a></li>
            </ul>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Category</a>
            <ul class="dropdown-menu">
              <li><a class="dropdown-item" href="/addaccount">Add Account</a></li>
              <li><a class="dropdown-item" href="#">Add Item</a></li>
            </ul>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="/auction">Auction</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="/luckywheel">LuckyWheel</a>
          </li>
        </ul>
        <div class="search-and-icons">
          <form class="d-flex mb-2 me-2" role="search">
            <input class="form-control me-2" type="search" aria-label="Search">
          </form>
          <div class="user-icons d-flex mb-2">
            <div class="wishlist position-relative">
              <i class="bi bi-bell" id="notificationIcon"></i>
              <div class="dropdown-menu p-3" id="notificationDropdown">
                <h6 class="dropdown-header">Notifications</h6>
                <div class="notification-item">Notification 1</div>
                <div class="notification-item">Notification 2</div>
                <div class="notification-item">Notification 3</div>
              </div>
            </div>
            <div class="cart">
              <i class="bi bi-heart"></i>
            </div>
            <div class="profile text-black dropdown" style="cursor: pointer" @click="toggleDropdown">
              <a class="d-flex align-items-center" @click.prevent="isLoggedIn ? null : goToLogin()">
                <i :class="isLoggedIn ? 'bi-person-check-fill' : 'bi-person'"></i>
              </a>
              <div v-if="isLoggedIn && showDropdown" class="dropdown-menu custom-dropdown-menu show">
                <a class="dropdown-item" href="/profile">Thông tin cá nhân</a>
                <a class="dropdown-item" href="/purchasedaccount">Lịch sử</a>
                <a class="dropdown-item" @click.prevent="logout" href="/login">Đăng xuất</a>
              </div>
            </div>
            
            
          </div>
        </div>
        <div class="contact-info d-md-flex">
          <p>+84 392822440 | <a href="https://github.com/RudeusGs/DoAn_CoSoNganh" target="_blank">github.com/RudeusGs/DoAn_CoSoNganh</a></p>
        </div>
      </div>
    </div>
  </nav>
</template>

<script lang="ts">
import router from '@/router';
import { onMounted, ref } from 'vue';

export default {
  setup() {
    const isLoggedIn = ref(false);
    const showDropdown = ref(false);
    const userFullName = ref('');

    onMounted(() => {
  const token = localStorage.getItem('token');
  if (token) {
    isLoggedIn.value = true;
    userFullName.value = localStorage.getItem('fullName') || ''; // Lấy tên đầy đủ người dùng từ localStorage
  }
});

    const toggleDropdown = () => {
      if (isLoggedIn.value) {
        showDropdown.value = !showDropdown.value;
      } else {
        goToLogin();
      }
    };
    const logout = () => {
  isLoggedIn.value = false;
  userFullName.value = '';
  localStorage.removeItem('token');
  localStorage.removeItem('fullName');
  showDropdown.value = false;
};


    const goToLogin = () => {
      router.push('/login');
    };

    return { isLoggedIn, showDropdown, logout, goToLogin, userFullName,toggleDropdown  };
  }
};
</script>

<style scoped>
.custom-dropdown-menu {
  right: auto;
  transform: translateX(-100%); /* Moves the dropdown fully to the left */
}
.navbar {
  box-shadow: 0 5px 5px rgba(0, 0, 0, 0.1);
}
.navbar .navbar-brand img {
  max-width: 100px;
}
.navbar .navbar-nav .nav-link {
  color: #000;
}
@media screen and (min-width: 1024px) {
  .navbar {
    letter-spacing: 0.1em;
  }
  .navbar .navbar-nav .nav-link {
    padding: 0.5em 1em;
  }
  .search-and-icons {
    width: 50%;
  }
  .search-and-icons form {
    flex: 1;
  }
}
@media screen and (min-width: 768px) {
  .navbar .navbar-brand img {
    max-width: 7em;
  }
  .navbar .navbar-collapse {
    display: flex;
    flex-direction: column-reverse;
    align-items: flex-end;
  }
  .search-and-icons {
    display: flex;
    align-items: center;
  }
}
.search-and-icons form input {
  border-radius: 0;
  height: 2em;
  background: #fff url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='grey' class='bi bi-search' viewBox='0 0 16 16'%3E%3Cpath d='M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z'/%3E%3C/svg%3E") no-repeat 95%;
}
.search-and-icons form input:focus {
  background: #fff;
  box-shadow: none;
}
.search-and-icons .user-icons div {
  padding-right: 1em;
}
.contact-info p,
.contact-info a {
  font-size: 0.9em;
  padding-right: 1em;
  color: grey;
}
.contact-info a {
  padding-right: 0;
}
#notificationDropdown {
  display: none;
  position: absolute;
  top: 100%;
  right: 0;
  width: 600px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  z-index: 1000;
  background-color: white;
  border: 1px solid #ddd;
  border-radius: 4px;
}

/* Styles for notification items */
.notification-item {
  padding: 10px;
  border-bottom: 1px solid #f1f1f1;
  cursor: pointer;
}

.notification-item:hover {
  background-color: #f9f9f9;
}

/* Responsive styles */
@media (max-width: 576px) {
  #notificationDropdown {
    width: 100vw; /* Full width on mobile */
    right: 0;
    left: 0;
  }
}

@media (max-width: 768px) {
  #notificationDropdown {
    width: 100%; /* Full width on smaller tablets */
    left: 0;
    right: 0;
  }
}
</style>
