<template>
    <div class="overlay">
      <div class="form-container">
        <div class="form-header">
          <h2>Nạp tiền thẻ cào</h2>
        </div>
        <form>
          <div class="form-group">
            <label for="provider">Nhà cung cấp</label>
            <select id="provider" class="form-control">
              <option value="viettel">VIETTEL</option>
              <option value="vinaphone">VINAPHONE</option>
              <option value="mobiphone">MOBIPHONE</option>
              <!-- Thêm các tùy chọn nhà cung cấp khác -->
            </select>
          </div>
  
          <div class="form-group">
            <label>Chọn mệnh giá</label>
            <div class="amounts">
              <button
                v-for="(amount, index) in amounts"
                :key="index"
                :class="['amount-btn', { active: selectedAmount === amount }]"
                @click="selectAmount(amount)"
                type="button"
              >
                {{ amount }}<br>Nhận 80%
              </button>
            </div>
          </div>
  
          <div class="form-group">
            <label>Mã số thẻ</label>
            <input type="text" class="form-control" placeholder="Nhập mã số thẻ của bạn">
          </div>
  
          <div class="form-group">
            <label>Số sê-ri</label>
            <input type="text" class="form-control" placeholder="Nhập mã số sê-ri trên thẻ">
          </div>
  
          <div class="form-warning">
            <p>*Chú ý: Nạp thẻ sai mệnh giá mất 50% giá trị thẻ.</p>
          </div>
  
          <button type="submit" class="submit-btn">Nạp ngay</button>
        </form>
      </div>
    </div>
  </template>
  
  <script>
  import { ref } from 'vue';
  
  export default {
    name: 'TopUpForm',
    setup() {
      const amounts = ref([10000, 20000, 30000, 50000, 100000, 200000, 300000, 500000, 1000000]);
      const selectedAmount = ref(10000); // Mặc định chọn 10,000 VND
  
      const selectAmount = (amount) => {
        selectedAmount.value = amount; // Cập nhật mệnh giá được chọn
      };
  
      return {
        amounts,
        selectedAmount,
        selectAmount,
      };
    },
  };
  </script>
  
  <style scoped>
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  
  body,
  html {
    height: 100%;
    font-family: Arial, sans-serif;
    font-size: 12px;
  }
  
  .overlay {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background-color: rgba(255, 255, 255, 0.5);
  }
  
  .form-container {
    background-color: white;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
    width: 100%;
    max-width: 600px;
  }
  
  .form-header {
    text-align: center;
    margin-bottom: 20px;
  }
  
  .form-group {
    margin-bottom: 10px;
  }
  
  .form-control {
    width: 100%;
    padding: 8px;
    border: 1px solid #ddd;
    border-radius: 5px;
  }
  
  .amounts {
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
    margin-top: 10px;
  }
  
  .amount-btn {
    flex: 1;
    padding: 15px;
    background-color: #f1f1f1;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    font-size: 12px;
    min-width: 120px;
    min-height: 80px;
    text-align: center;
  }
  
  .amount-btn.active {
    background-color: #008080;
    color: white;
  }
  
  .form-warning {
    color: red;
    font-size: 10px;
    margin-bottom: 10px;
  }
  
  .submit-btn {
    width: 100%;
    padding: 8px;
    background-color: #008080;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
  }
  </style>
  