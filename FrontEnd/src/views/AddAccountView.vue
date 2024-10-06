<template>
  <div class="add-account-form container mt-5 mb-5">
    <form @submit.prevent="submitForm" class="p-4 rounded shadow-sm bg-light">
        <h2 class="text-center mb-4">Add New Account</h2>

        <div class="form-group mb-3">
            <label for="accountName" class="form-label">Account Name</label>
            <input v-model="form.accountName" type="text" id="accountName" class="form-control" required />
        </div>

        <div class="form-group mb-3">
            <label for="accountPassword" class="form-label">Password</label>
            <input v-model="form.accountPassword" type="password" id="accountPassword" class="form-control" required />
        </div>

        <div class="form-group mb-3">
            <label for="status" class="form-label">Status</label>
            <select v-model="form.status" id="status" class="form-select" required>
                <option value="true">Active</option>
                <option value="false">Inactive</option>
            </select>
        </div>

        <div class="form-group mb-3">
            <label for="server" class="form-label">Server</label>
            <select v-model="form.server" id="server" class="form-select" required>
                <option v-for="n in 10" :key="n" :value="n">Server {{ n }}</option>
            </select>
        </div>

        <div class="form-group mb-3">
            <label for="planet" class="form-label">Planet</label>
            <select v-model="form.planet" id="planet" class="form-select" required>
                <option value="Trái đất">Trái đất</option>
                <option value="Namec">Namec</option>
                <option value="Xayda">Xayda</option>
            </select>
        </div>

        <div class="form-group mb-3">
            <label for="price" class="form-label">Price</label>
            <input v-model="form.price" type="number" id="price" class="form-control" required />
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
        </div>

        <div class="d-grid">
            <button type="submit" class="btn btn-primary">Add Account</button>
        </div>
    </form>

    <div v-if="errorMessage" class="alert alert-danger mt-3">{{ errorMessage }}</div>
    <div v-if="successMessage" class="alert alert-success mt-3">{{ successMessage }}</div>
</div>

</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { type AddGameAccountModel } from '@/models/gameaccount-model';  // Ensure this path is correct
import { addGameAccount } from '@/api/addgameaccount.api'; // Ensure the correct import for the API function

export default defineComponent({
  name: 'AddAccount',
  setup() {
    const form = ref<AddGameAccountModel>({
      accountName: '',
      accountPassword: '',
      status: true,
      server: '',
      earring: false,
      planet: '',
      price: 0,
    });

    const selectedFiles = ref<File[]>([]);
    const errorMessage = ref<string | null>(null);
    const successMessage = ref<string | null>(null);

    const handleFileChange = (event: Event) => {
      const files = (event.target as HTMLInputElement).files;
      if (files) {
        selectedFiles.value = Array.from(files);
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

        // Append earring only if it is true
        if (form.value.earring) {
          formData.append('Earring', 'true');
        }

        // Append files if any
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
        accountName: '',
        accountPassword: '',
        status: true,
        server: '',
        earring: false,
        planet: '',
        price: 0,
      };
      selectedFiles.value = [];
    };

    return {
      form,
      selectedFiles,
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
  max-width: 600px;
  margin: 0 auto;
}
</style>
