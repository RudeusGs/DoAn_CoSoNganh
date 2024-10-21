<template>
    <div class="banking-container container mt-5 mb-5">
      <h2 class="text-center mb-4">Chuyển Khoản Ngân Hàng</h2>
      
      <div class="row">
        <!-- Hình Ảnh Mã QR -->
        <div class="col-md-6 text-center mb-4">
          <img 
            src="../assets/z5951434463237_b62a3f3117d176c1a8b64cbe73533f2a.jpg"
            alt="Quét mã QR để chuyển khoản" 
            class="img-fluid qr-code" 
          />
          <div class="mt-3">
            <button class="btn btn-primary" @click="downloadQRCode">
              <i class="fa fa-download me-2"></i>Tải Mã QR
            </button>
          </div>
        </div>
        
        <!-- Nội Dung Chuyển Khoản -->
        <div class="col-md-6">
          <div class="transfer-info p-4 bg-light rounded shadow-sm">
            <h4>Thông Tin Chuyển Khoản</h4>
            <hr />
            <p><strong>Tên Ngân Hàng:</strong> <span class="text-primary">{{ bankName }}</span></p>
            <p><strong>Số Tài Khoản:</strong> <span class="text-primary">{{ accountNumber }}</span></p>
            <p><strong>Tên Chủ Tài Khoản:</strong> <span class="text-primary">{{ accountHolder }}</span></p>
            <p><strong>Số Tiền Cần Chuyển:</strong> <span class="text-danger">{{ transferAmount }}</span></p>
            <p><strong>Nội Dung Chuyển Khoản:</strong> <span class="text-secondary">{{ transferContent }}</span></p>
            <p><strong>Thời Gian Hoàn Tất:</strong> <span class="text-secondary">{{ completionTime }}</span></p>
            <hr />
            <p class="text-success"><i class="fa fa-check-circle me-2"></i>Vui lòng đảm bảo rằng bạn đã nhập đúng thông tin để tránh mất mát tài sản.</p>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref } from 'vue';
  
  export default defineComponent({
    name: 'BankingComponent',
    setup() {
      // Thông tin ngân hàng và chuyển khoản
      const bankName = 'Ngân hàng Thương mại cổ phần Đầu tư và Phát triển Việt Nam(BIDV)';
      const accountNumber = '6505136573';
      const accountHolder = 'Ngô Trần Nguyên Quân';
      const transferAmount = '1,000,000,000 VNĐ';
      const transferContent = 'Thanh toán dịch vụ';
      const completionTime = 'Trong vòng 24 giờ';
      
      // URL của mã QR (có thể là base64 hoặc đường dẫn đến hình ảnh)
      const qrCodeUrl = 'https://via.placeholder.com/250x250.png?text=QR+Code';
      
      // Phương thức tải xuống mã QR
      const downloadQRCode = () => {
        const link = document.createElement('a');
        link.href = qrCodeUrl;
        link.download = 'qr-code.png';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
      };
      
      return {
        bankName,
        accountNumber,
        accountHolder,
        transferAmount,
        transferContent,
        completionTime,
        qrCodeUrl,
        downloadQRCode,
      };
    },
  });
  </script>
  
  <style scoped>
  .banking-container {
    max-width: 900px;
  }
  
  .qr-code {
    max-width: 250px;
    border: 2px solid #007bff;
    border-radius: 10px;
  }
  
  .transfer-info h4 {
    color: #007bff;
  }
  
  .transfer-info p {
    font-size: 16px;
  }
  
  .transfer-info .text-primary {
    font-weight: bold;
  }
  
  .transfer-info .text-danger {
    font-size: 18px;
  }
  
  .transfer-info .text-secondary {
    font-style: italic;
  }
  </style>
  