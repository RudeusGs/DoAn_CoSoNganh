<template>
  <div class="add-account-post container mt-5 mb-5 p-3 bg-white rounded shadow-sm">

    <!-- Post content -->
    <div class="post-content mb-3">
      <textarea v-model="form.content" id="content" class="form-control" rows="3" placeholder="Nội dung của bài viết" required></textarea>
    </div>

    <!-- Account Details -->
    <div class="row mb-3">
      <div class="col-md-6">
        <label for="accountName" class="form-label">Account Name</label>
        <input v-model="form.accountName" type="text" id="accountName" class="form-control" required />
      </div>
      <div class="col-md-6">
        <label for="accountPassword" class="form-label">Password</label>
        <input v-model="form.accountPassword" type="password" id="accountPassword" class="form-control" required />
      </div>
    </div>

    <!-- Additional Details -->
    <div class="row mb-3">
      <div class="col-md-6">
        <label for="server" class="form-label">Server</label>
        <select v-model="form.server" id="server" class="form-select" required>
          <option v-for="n in 10" :key="n" :value="n">Server {{ n }}</option>
        </select>
      </div>
      <div class="col-md-6">
        <label for="planet" class="form-label">Planet</label>
        <select v-model="form.planet" id="planet" class="form-select" required>
          <option value="Trái đất">Trái đất</option>
          <option value="Namec">Namec</option>
          <option value="Xayda">Xayda</option>
        </select>
      </div>
    </div>

    <!-- Price and Options -->
    <div class="row mb-3">
      <div class="col-md-6">
        <label for="price" class="form-label">Price</label>
        <input v-model="form.price" type="number" id="price" class="form-control" required />
      </div>
      <div class="col-md-6 d-flex align-items-center">
        <div class="form-check form-switch">
          <input v-model="form.earring" type="checkbox" id="earring" class="form-check-input" />
          <label class="form-check-label" for="earring">Earring</label>
        </div>
      </div>
    </div>

    <!-- File Upload and Preview -->
    <div class="form-group mb-3">
      <label for="files" class="form-label">Upload Images</label>
      <input type="file" @change="handleFileChange" multiple class="form-control" />
      <div class="image-preview mt-2 d-flex flex-wrap">
        <img v-for="image in imagePreviews" :src="image" :key="image" class="img-thumbnail me-2" style="max-width: 80px;" />
      </div>
    </div>

    <!-- Submit button -->
    <div class="d-grid mb-2">
      <button type="submit" class="btn btn-primary btn-block" @click="submitForm">Post</button>
    </div>

    <!-- Error and Success messages -->
    <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>
    <div v-if="successMessage" class="alert alert-success">{{ successMessage }}</div>
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
      content: '',
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
        formData.append('Content', form.value.content);

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
        content: '',
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
.add-account-post {
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  padding: 20px;
}

.post-header {
  display: flex;
  align-items: center;
  margin-bottom: 15px;
}

.avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}

.post-user-info {
  margin-left: 10px;
}

.post-user-info strong {
  font-size: 16px;
  color: #333;
}

.post-time {
  font-size: 12px;
  color: #777;
}

.post-content {
  margin-bottom: 20px;
}

.post-content textarea {
  width: 100%;
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #ddd;
  resize: none;
}

.form-label {
  font-weight: bold;
  color: #333;
}

.form-control,
.form-select {
  border-radius: 8px;
  border: 1px solid #ddd;
  padding: 10px;
}

.image-preview img {
  max-width: 80px;
  max-height: 80px;
  border-radius: 8px;
  object-fit: cover;
}

.btn-block {
  display: block;
  width: 100px;
}

.btn-primary {
  background-color: #1877f2;
  border-color: #1877f2;
}

.btn-primary:hover {
  background-color: #165cbb;
}

.shadow-lg {
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
}
</style>
