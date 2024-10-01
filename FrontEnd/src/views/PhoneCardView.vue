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
                        <option value="viettel">VINAPHONE</option>
                        <option value="viettel">MOBIPHONE</option>
                        <!-- Thêm các tùy chọn nhà cung cấp khác -->
                    </select>
                </div>
    
                <div class="form-group">
                    <label>Chọn mệnh giá</label>
                    <div class="amounts">
                        <button class="amount-btn active">10,000<br>Nhận 80%</button>
                        <button class="amount-btn">20,000<br>Nhận 80%</button>
                        <button class="amount-btn">30,000<br>Nhận 80%</button>
                        <button class="amount-btn">50,000<br>Nhận 80%</button>
                        <button class="amount-btn">100,000<br>Nhận 80%</button>
                        <button class="amount-btn">200,000<br>Nhận 80%</button>
                        <button class="amount-btn">300,000<br>Nhận 80%</button>
                        <button class="amount-btn">500,000<br>Nhận 80%</button>
                        <button class="amount-btn">1,000,000<br>Nhận 80%</button>
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
<style scoped>
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

body, html {
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

.tab {
    display: flex;
    justify-content: center;
    margin-top: 10px;
}

.tablinks {
    background-color: #f1f1f1;
    border: none;
    padding: 8px 15px; /* Điều chỉnh padding cho nhỏ hơn */
    margin-right: 10px;
    cursor: pointer;
}

.tablinks.active {
    background-color: #008080;
    color: white;
}

.form-group {
    margin-bottom: 10px; /* Giảm khoảng cách dưới cho nhóm biểu mẫu */
}

.form-control {
    width: 100%;
    padding: 8px; /* Giảm padding trong input */
    border: 1px solid #ddd;
    border-radius: 5px;
}

.amounts {
    display: flex;
    flex-wrap: wrap;
    gap: 8px; /* Giảm khoảng cách giữa các nút mệnh giá */
    margin-top: 10px;
}

.amount-btn {
    flex: 1;
    padding: 15px; /* Giảm padding cho nút */
    background-color: #f1f1f1;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    font-size: 12px; /* Giảm kích thước font chữ cho nút xuống còn 12px */
    min-width: 120px; /* Điều chỉnh chiều rộng tối thiểu */
    min-height: 80px; /* Điều chỉnh chiều cao tối thiểu */
    text-align: center;
}

.amount-btn.active {
    background-color: #008080;
    color: white;
}

.amount-value {
    font-size: 10px; /* Kích thước nhỏ cho giá trị tiền */
    font-weight: bold;
}

.amount-receive {
    font-size: 10px; /* Kích thước nhỏ cho "Nhận 80%" */
    color: #666;
}

.form-warning {
    color: red;
    font-size: 10px; /* Kích thước chữ nhỏ cho thông báo */
    margin-bottom: 10px; /* Giảm khoảng cách dưới cho thông báo */
}

.submit-btn {
    width: 100%;
    padding: 8px; /* Giảm padding cho nút nạp tiền */
    background-color: #008080;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}



</style>