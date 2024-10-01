<template>
    <div>
      <h2>Game Accounts</h2>
      <table class="table">
        <thead>
          <tr>
            <th>Account Name</th>
            <th>Price</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(account, index) in accounts" :key="index">
            <td>{{ account.accountName }}</td>
            <td>{{ account.price }}</td>
            <td>{{ account.status }}</td>
            <td>
              <button @click="editAccount(index)" class="btn btn-primary">Edit</button>
              <button @click="deleteAccount(index)" class="btn btn-danger">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
      <router-link to="/add" class="btn btn-success">Add New Account</router-link>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        accounts: [
          { accountName: 'Account1', price: '100', status: 'Available' },
          { accountName: 'Account2', price: '200', status: 'Sold' },
          { accountName: 'Account3', price: '150', status: 'Available' },
        ],
      };
    },
    methods: {
      editAccount(index) {
        this.$router.push({ path: `/edit/${index}` });
      },
      deleteAccount(index) {
        if (confirm('Are you sure you want to delete this account?')) {
          this.accounts.splice(index, 1); // Remove account from the list
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .table {
    margin-top: 20px;
  }
  </style>
  