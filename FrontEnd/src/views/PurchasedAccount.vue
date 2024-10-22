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
            <th scope="col">Số tiền</th>
            <th scope="col">Báo cáo</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(account, index) in purchasedAccounts" :key="index">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ account.username }}</td>
            <td>{{ account.password }}</td>
            <td>{{ formatDate(account.purchaseDate) }}</td>
            <td>{{account.price}}</td>
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
  price: string;
}

export default defineComponent({
  setup() {
    const purchasedAccounts = ref<Account[]>([]);

    const fetchPurchasedAccounts = async () => {
  try {
    const user = localStorage.getItem('user');
    if (user) {
      const userId = JSON.parse(user).id;
      console.log("UserId:", userId);
      const response = await purchasedaccountApi.getPurchasedAccountsByUser(userId);
      
      console.log("Full API Response:", response);

      if (response && response.data && response.data.isSuccess) {
        purchasedAccounts.value = response.data.data.map((account: any) => ({
          username: account.accountName,
          password: account.accountPassword,
          purchaseDate: account.createdDate,
          price : account.price
        }));
        console.log("Purchased accounts:", purchasedAccounts.value);
      } else {
        console.error('Failed to fetch purchased accounts:', response?.data?.message);
      }
    } else {
      console.error('User not found in localStorage');
    }
  } catch (error) {
    console.error('Error fetching purchased accounts:', error);
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
  max-width: 1200px;
  margin: auto;
}

.table {
  border-radius: 15px;
  overflow: hidden;
  width: 100%; 
}

th, td {
  padding: 0.5rem; 
  font-size: 0.9rem;
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
  max-height: 700px;
  overflow-y: auto;
}

</style>
