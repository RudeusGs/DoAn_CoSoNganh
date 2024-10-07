<template>
    <div class="container">
      <div class="row">
        <div class="col-12">
          <h1 class="text-center">Admin Panel</h1>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <table class="table table-striped">
            <thead>
              <tr>
                <th>#</th>
                <th>Name</th>
                <th>Email</th>
                <th>Role</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="user in users" :key="user.id">
                <td>{{ user.id }}</td>
                <td>{{ user.name }}</td>
                <td>{{ user.email }}</td>
                <td>{{ user.role }}</td>
                <td>
                  <button @click="editUser(user)" class="btn btn-primary">Edit</button>
                  <button @click="deleteUser(user.id)" class="btn btn-danger">Delete</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
</template>
  
  <script>
  export default {
    data() {
      return {
        users: [
          { id: 1, name: "Nguyen Van A", email: "a@example.com", role: "Admin" },
          { id: 2, name: "Tran Thi B", email: "b@example.com", role: "User" },
          // More users can be added here
        ],
      };
    },
    methods: {
      editUser(user) {
        // Logic for editing the user goes here
        console.log("Edit user:", user);
      },
      deleteUser(userId) {
        // Logic for deleting the user goes here
        this.users = this.users.filter(user => user.id !== userId);
        console.log("User deleted:", userId);
      },
    },
  };
  </script>
  