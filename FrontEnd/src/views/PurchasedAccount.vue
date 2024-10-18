<template>
  <div class="container-fluid py-5">
    <h1 class="text-center mb-4">Danh sách tài khoản đã mua</h1>
    <div class="table-responsive">
      <table class="table table-bordered table-hover shadow-sm rounded-lg" v-if="purchasedAccounts.length > 0">
        <thead class="thead-dark">
          <tr>
            <th scope="col">#</th>
            <th scope="col">Username</th>
            <th scope="col">Password</th>
            <th scope="col">Ngày mua</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(account, index) in purchasedAccounts" :key="index">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ account.username }}</td>
            <td>{{ account.password }}</td>
            <td>{{ formatDate(account.purchaseDate) }}</td>
          </tr>
        </tbody>
      </table>
      <p v-else>Không có tài khoản nào được mua.</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import purchasedaccountApi from '@/api/purchasedaccountservice.api';

interface Account {
  username: string;
  password: string;
  purchaseDate: string;
}

export default defineComponent({
  setup() {
    const purchasedAccounts = ref<Account[]>([]);

    const fetchPurchasedAccounts = async () => {
      try {
        const response = await purchasedaccountApi.getAllPurchasedAccount();
        if (response && response.data.result.isSuccess) {
          purchasedAccounts.value = response.data.result.data.map((account: any) => ({
            username: account.accountName,
            password: account.accountPassword,
            purchaseDate: account.createdDate,
          }));
        }
      } catch (error) {
        console.error("Lỗi khi lấy tài khoản đã mua:", error);
      }
    };

    const formatDate = (dateString: string) => {
      const date = new Date(dateString);
      return date.toLocaleDateString();
    };

    onMounted(() => {
      fetchPurchasedAccounts();
    });

    return {
      purchasedAccounts,
      formatDate,
    };
  },
});
</script>

<style scoped>
.container-fluid {
  max-width: 1200px; /* Tăng độ rộng của bảng */
  margin: auto;
}

.table {
  border-radius: 15px;
  overflow: hidden;
  width: 100%; /* Đảm bảo bảng chiếm toàn bộ chiều rộng */
}

th, td {
  padding: 0.5rem; /* Giảm khoảng cách giữa các hàng */
  font-size: 0.9rem; /* Giảm kích thước font để hàng ngắn hơn */
  text-align: left;
}

th.thead-dark {
  background-color: #343a40;
  color: #fff;
}

.shadow-sm {
  box-shadow: 0 .125rem .25rem rgba(0,0,0,.075)!important;
}

.rounded-lg {
  border-radius: 15px !important;
}

.table-responsive {
  max-height: 600px; /* Tăng chiều dài của bảng */
  overflow-y: auto; /* Đảm bảo bảng cuộn được nếu quá dài */
}

</style>
