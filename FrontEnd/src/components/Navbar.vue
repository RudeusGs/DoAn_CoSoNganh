<template>
  <nav class="navbar navbar-expand-md bg-body-tertiary">
      <div class="container-xl">
          <a class="navbar-brand" href="#">
              <a href=""><img src="/public/LogoWebsite.png" alt=""></a>
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
                      <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                          Payin
                      </a>
                      <ul class="dropdown-menu">
                          <li><a class="dropdown-item" href="/phonecard">Phone Card</a></li>
                          <li><a class="dropdown-item" href="#">Banking</a></li>
                      </ul>
                  </li>
                  <li class="nav-item dropdown">
                      <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                          Category
                      </a>
                      <ul class="dropdown-menu">
                          <li><a class="dropdown-item" href="/addaccount">Add Account</a></li>
                          <li><a class="dropdown-item" href="#">Add Item</a></li>
                          <li><a class="dropdown-item" href="#">Buy Account</a></li>
                          <li><a class="dropdown-item" href="#">Buy Item</a></li>
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
                          <div class="wishlist position-relative">
                              <div class="dropdown-menu p-3" id="notificationDropdown">
                                  <h6 class="dropdown-header">Thông báo</h6>
                                  <div class="notification-item">Thông báo 1</div>
                                  <div class="notification-item">Thông báo 2</div>
                                  <div class="notification-item">Thông báo 3</div>
                              </div>
                          </div>
                      </div>
                      <div class="cart"><i class="bi bi-heart"></i></div>
                      <a class="profile text-black" href="/login"><i class="bi bi-person"></i></a>
                  </div>
              </div>
              <div class="contact-info d-md-flex">
                  <p>+84 392822440 | github.com/RudeusGs/DoAn_CoSoNganh</p>
              </div>
          </div>
      </div>
  </nav>
</template>


<script lang="ts">
  document.addEventListener("DOMContentLoaded", () => {
    const notificationIcon = document.getElementById("notificationIcon");
    const notificationDropdown = document.getElementById("notificationDropdown");

    if (notificationIcon && notificationDropdown) {
        notificationIcon.addEventListener("click", () => {
            if (notificationDropdown.style.display === "none" || notificationDropdown.style.display === "") {
                notificationDropdown.style.display = "block";
            } else {
                notificationDropdown.style.display = "none";
            }
        });
        document.addEventListener("click", (event: MouseEvent) => {
            const target = event.target as HTMLElement;
            if (!target.closest(".wishlist") && !target.closest("#notificationDropdown")) {
                notificationDropdown.style.display = "none";
            }
        });
    }
});

</script>

<style scoped>
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
    background: #fff
      url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='grey' class='bi bi-search' viewBox='0 0 16 16'%3E%3Cpath d='M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z'/%3E%3C/svg%3E")
      no-repeat 95%;
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

/* Kiểu dáng cho các mục thông báo */
.notification-item {
    padding: 10px;
    border-bottom: 1px solid #f1f1f1;
    cursor: pointer;
}

.notification-item:hover {
    background-color: #f9f9f9;
}

/* Responsive khi màn hình nhỏ hơn 576px (dành cho thiết bị di động) */
@media (max-width: 576px) {
    #notificationDropdown {
        width: 100vw; /* Chiều rộng menu chiếm 100% chiều rộng màn hình */
        right: 0;     /* Đảm bảo menu vẫn dính vào bên phải */
        left: 0;      /* Căn giữa theo chiều ngang */
    }
}

/* Responsive cho màn hình nhỏ hơn 768px (máy tính bảng nhỏ) */
@media (max-width: 768px) {
    #notificationDropdown {
        width: 100%; /* Menu chiếm hết chiều ngang của màn hình khi ở kích thước này */
        left: 0;     /* Đảm bảo menu nằm giữa */
        right: 0;
    }
}


</style>