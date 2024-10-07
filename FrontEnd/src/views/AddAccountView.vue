<template>
  <div class="add-account-form container mt-5 mb-5">
    <form @submit.prevent="submitForm" class="p-4 rounded shadow bg-white">
      <h2 class="text-center mb-4">Add New Account</h2>
      
      <div class="row">
        <div class="col-md-6 mb-3">
          <label for="accountName" class="form-label">Account Name</label>
          <input v-model="form.accountName" type="text" id="accountName" class="form-control" required />
        </div>

        <div class="col-md-6 mb-3">
          <label for="accountPassword" class="form-label">Password</label>
          <input v-model="form.accountPassword" type="password" id="accountPassword" class="form-control" required />
        </div>
      </div>

      <div class="row">
        <div class="col-md-6 mb-3">
          <label for="status" class="form-label">Status</label>
          <select v-model="form.status" id="status" class="form-select" required>
            <option value="true">Active</option>
            <option value="false">Inactive</option>
          </select>
        </div>

        <div class="col-md-6 mb-3">
          <label for="server" class="form-label">Server</label>
          <select v-model="form.server" id="server" class="form-select" required>
            <option v-for="n in 10" :key="n" :value="n">Server {{ n }}</option>
          </select>
        </div>
      </div>

      <div class="row">
        <div class="col-md-6 mb-3">
          <label for="planet" class="form-label">Planet</label>
          <select v-model="form.planet" id="planet" class="form-select" required>
            <option value="Trái đất">Trái đất</option>
            <option value="Namec">Namec</option>
            <option value="Xayda">Xayda</option>
          </select>
        </div>

        <div class="col-md-6 mb-3">
          <label for="price" class="form-label">Price</label>
          <input v-model="form.price" type="number" id="price" class="form-control" required />
        </div>
      </div>

      <div class="form-group mb-3">
        <div class="form-check">
          <input v-model="form.earring" type="checkbox" id="earring" class="form-check-input" />
          <label class="form-check-label" for="earring">Earring</label>
        </div>
      </div>

      <div class="form-group mb-4">
        <label for="files" class="form-label">Upload Images</label>
        <input type="file" @change="handleFileChange" multiple class="form-control" />
        <div class="image-preview mt-3">
          <img v-for="image in imagePreviews" :src="image" :key="image" class="img-thumbnail me-2" style="max-width: 100px;" />
        </div>
      </div>

      <div class="d-grid">
        <button type="submit" class="btn btn-secondary btn-block">Add Account</button>
      </div>
    </form>

    <div v-if="errorMessage" class="alert alert-danger mt-3">{{ errorMessage }}</div>
    <div v-if="successMessage" class="alert alert-success mt-3">{{ successMessage }}</div>
  </div>
</template>


<script lang="ts">
import { defineComponent, ref } from 'vue';
import { type AddGameAccountModel } from '@/models/gameaccount-model'; 
import { addGameAccount } from '@/api/addgameaccount.api';

export default defineComponent({
  name: 'AddAccount',
  setup() {
    const form = ref<AddGameAccountModel>({
      id: 0,
      accountName: '',
      accountPassword: '',
      status: true,
      server: '',
      earring: false,
      planet: '',
      price: 0,
      posterName: '',      
      createdDate: '',
    });

    const selectedFiles = ref<File[]>([]);
    const imagePreviews = ref<string[]>([]);
    const errorMessage = ref<string | null>(null);
    const successMessage = ref<string | null>(null);

    const handleFileChange = (event: Event) => {
      const files = (event.target as HTMLInputElement).files;
      if (files) {
        selectedFiles.value = Array.from(files);
        imagePreviews.value = selectedFiles.value.map(file => URL.createObjectURL(file));
      }
    };

    const submitForm = async () => {
      try {
        const formData = new FormData();
        formData.append('AccountName', form.value.accountName);
        formData.append('AccountPassword', form.value.accountPassword);
        formData.append('Status', form.value.status.toString());
        formData.append('Server', form.value.server);
        formData.append('Planet', form.value.planet);
        formData.append('Price', form.value.price.toString());

        if (form.value.earring) {
          formData.append('Earring', 'true');
        }
        if (selectedFiles.value.length) {
          selectedFiles.value.forEach(file => {
            formData.append('Files', file);
          });
        }

        const result = await addGameAccount(formData);
        successMessage.value = 'Game account added successfully!';
        errorMessage.value = null;
        resetForm();
      } catch (error: any) {
        errorMessage.value = error.message;
        successMessage.value = null;
      }
    };

    const resetForm = () => {
      form.value = {
        id: 0,
        accountName: '',
        accountPassword: '',
        status: true,
        server: '',
        earring: false,
        planet: '',
        price: 0,
        posterName: '',         
        createdDate: '',
      };
      selectedFiles.value = [];
      imagePreviews.value = [];
    };

    return {
      form,
      selectedFiles,
      imagePreviews,
      errorMessage,
      successMessage,
      handleFileChange,
      submitForm,
    };
  },
});
</script>


<style scoped>
.add-account-form {
  background-color: #f8f9fa;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.form-label {
  font-weight: bold;
}

.btn-block {
  display: block;
  width: 100%;
}

.shadow {
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.bg-white {
  background-color: #fff;
}
</style>