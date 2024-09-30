<template>
    <nav class="navbar navbar-expand-md bg-body-tertiary">
        <div class="container-xl">
          <a class="navbar-brand" href="#">
            <img src="/public/LogoWebsite.png" alt="">
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
                  <li><a class="dropdown-item" href="#">Phone Card</a></li>
                  <li><a class="dropdown-item" href="#">Banking</a></li>
                </ul>
              </li>
              <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                  Category
                </a>
                <ul class="dropdown-menu">
                  <li><a class="dropdown-item" href="#">Add Account</a></li>
                  <li><a class="dropdown-item" href="#">Add Item</a></li>
                  <li><a class="dropdown-item" href="#">Buy Account</a></li>
                  <li><a class="dropdown-item" href="#">Buy Item</a></li>
                </ul>
              </li>
              <li class="nav-item">
                <a class="nav-link">Auction</a>
              </li>
              <li class="nav-item">
                <a class="nav-link">LuckyWheel</a>
              </li>
            </ul>
            <div class="search-and-icons">
              <form class="d-flex mb-2 me-2" role="search">
                <input class="form-control me-2" type="search" aria-label="Search">
              </form>
              <div class="user-icons d-flex mb-2">                
                <div class="wishlist"><i class="bi bi-bell"></i></div>
                <div class="cart"><i class="bi bi-heart"></i></div>
                <a class="profile text-black" href="/login"><i class="bi bi-person"></i></a>
              </div>
            </div>
            <div class="contact-info d-md-flex">
              <p>+84 392822440 | github.com/RudeusGs/DoAn_CoSoNganh </p>
            </div>
          </div>
        </div>
      </nav>
    
</template>

<script lang="js">

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
  
</style>