<template>
    <div class="vh-100 d-flex align-items-center justify-content-center">
        <div class="auction-container w-100">
            <div class="row justify-content-center">
                <div class="col-md-4 mb-3 text-center">
                    <h4>Participants</h4>
                    <div class="participant-list">
                        <p>User1</p>
                        <p>User2</p>
                        <p>User3</p>
                    </div>
                </div>
                <div class="col-md-8 text-center">
                    <div class="auction-details mx-auto">
                        <h3>Item Auction</h3>
                        <img src="../assets/about.png" alt="Auction Item" class="img-fluid mb-3">
                        <div class="auction-info mb-3">
                            <p><strong>Auction Info:</strong></p>
                            <p>Starting Bid: $50</p>
                            <p>Current Bid: $75</p>
                            <p>Time Remaining: 2 hours 15 minutes</p>
                        </div>
                        <form>
                            <div class="form-group">
                                <input type="number" class="form-control" placeholder="Enter your bid">
                            </div>
                            <button type="submit" class="btn btn-primary">Place Bid</button>
                        </form>
                        <!-- User information -->
                       
                    </div>
                    
                </div>
                
            </div>
            
        </div>
        <div class="user-info ms-5">
            <p><strong>Họ và tên:</strong> Nguyễn Văn A</p>
            <p><strong>Số dư tài khoản:</strong> $1000</p>
        </div>
    </div>
    
</template>

<script lang="ts">
</script>

<style scoped>
.vh-100 {
    height: 100vh;
}

.auction-container {
    max-width: 800px;
    width: 100%;
    background: white;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.participant-list {
    max-height: 150px;
    overflow-y: auto;
    border: 1px solid #dee2e6;
    border-radius: 5px;
    padding: 10px;
}

.btn-primary {
    width: 100%;
}

.user-info {
    background-color: #f8f9fa;
    padding: 10px;
    border-radius: 5px;
    border: 1px solid #dee2e6;
}
</style>
