<template>
  <div class="container-fluid">
    <div class="row justify-content-center align-items-center min-vh-100">
      <div class="col-md-6 col-lg-4 shadow-lg p-4 rounded login_form">
        <div class="text-center mb-4">
          <span class="company__logo">
            <h2><span class="fa fa-android"></span></h2>
          </span>
        </div>
        <h2 class="text-center text-black mb-4">Login</h2>
        <form class="form-group" @submit.prevent="handleLogin">
          <div class="mb-3">
            <input
              v-model="userName"
              type="text"
              name="username"
              id="username"
              class="form-control form__input"
              placeholder="Username"
              required
            />
          </div>
          <div class="mb-3">
            <input
              v-model="password"
              type="password"
              name="password"
              id="password"
              class="form-control form__input"
              placeholder="Password"
              required
            />
          </div>
          <div class="form-check mb-3 text-start">
            <input type="checkbox" class="form-check-input" id="remember_me" />
            <label class="form-check-label" for="remember_me">Remember Me!</label>
          </div>
          <div class="d-grid mb-3">
            <button type="submit" class="btn btn-primary btn-block">Submit</button>
          </div>
        </form>
        <p class="text-center">
          Don't have an account? <a href="/register">Register Here</a>
        </p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { useRouter } from "vue-router";
import Cookies from "js-cookie";
import authApi from "@/api/authenticate.api";
import { userStore } from "../stores/auth";

export default defineComponent({
  setup() {
    const user = userStore();
    const userName = ref("");
    const password = ref("");
    const router = useRouter();

    const handleLogin = async () => {
      if (userName.value && password.value) {
        try {
          const loginModel = {
            userName: userName.value,
            password: password.value,
          };

          const response = await authApi.login(loginModel);

          if (response && response.result?.isSuccess) {
            user.login({
              userId: response.result.data.userId,
              username: response.result.data.username,
              email: response.result.data.email,
              fullName: response.result.data.fullName,
            });
            Cookies.set("token", response.result.data.token);
            router.push("/");
          } else {
            alert("Sai tài khoản hoặc mật khẩu.");
          }
        } catch (error) {
          console.error("Error during login:", error);
          alert("Bạn chưa vào chạy API");
        }
      } else {
        alert("Tài khoản và mật khẩu không được để trống.");
      }
    };

    return {
      userName,
      password,
      handleLogin,
    };
  },
});
</script>


<style scoped>
body {
  background-color: #f4f6f9;
}
.company__logo h2 {
  color: #008080;
  font-size: 3rem;
}
.company_title {
  color: #333;
  font-weight: bold;
  margin-top: 0.5em;
}
.login_form {
  background-color: #fff;
  border-radius: 20px;
  padding: 2em;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
}
.form__input {
  padding: 1em;
  border-radius: 5px;
  border: 1px solid #ccc;
  transition: all 0.3s ease;
}
.form__input:focus {
  border-color: #008080;
  box-shadow: 0 0 5px rgba(0, 128, 128, 0.3);
}
.btn {
  background-color: #008080;
  color: #fff;
  font-weight: bold;
  padding: 0.75em;
  border-radius: 25px;
  border: none;
  transition: background-color 0.3s ease;
}
.btn:hover {
  background-color: #006666;
}
@media (max-width: 768px) {
  .login_form {
    margin: 1em;
    padding: 2em;
  }
}
</style>
