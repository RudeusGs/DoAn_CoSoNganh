<template>
  <div class="container mt-5">
    <h1 class="text-center mb-4">Danh sách tài khoản đã mua</h1>
    <table class="table table-bordered table-hover shadow-sm rounded-lg" v-if="purchasedAccounts.length > 0">
      <thead>
        <tr>
          <th class="bg-light">Username</th>
          <th class="bg-light">Password</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(account, index) in purchasedAccounts" :key="index">
          <td>{{ account.username }}</td>
          <td>{{ account.password }}</td>
        </tr>
      </tbody>
    </table>
    <p v-else>Không có tài khoản nào được mua.</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
interface Account {
  username: string;
  password: string;
}

export default defineComponent({
  setup() {
    // Chỉ định kiểu dữ liệu cho danh sách tài khoản đã mua
    const purchasedAccounts = ref<Account[]>([]);

    // Lấy danh sách tài khoản đã mua từ Local Storage khi component được mount
    onMounted(() => {
      const storedAccounts = JSON.parse(localStorage.getItem('purchasedAccounts') || '[]');
      purchasedAccounts.value = storedAccounts;
    });

    return {
      purchasedAccounts,
    };
  },
});
</script>


<style scoped>
.container {
  max-width: 600px;
  margin: auto;
}

.table {
  border-radius: 15px;
  overflow: hidden;
}

th, td {
  padding: 1rem;
  font-size: 1.2rem;
  text-align: left;
}

th.bg-light {
  background-color: #f8f9fa;
}

.shadow-sm {
  box-shadow: 0 .125rem .25rem rgba(0,0,0,.075)!important;
}

.rounded-lg {
  border-radius: 15px !important;
}
</style>
