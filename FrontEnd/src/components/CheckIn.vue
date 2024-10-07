<template>
    <div class="check-in">
        <h1>Check-In</h1>
        <form @submit.prevent="handleCheckIn">
            <div>
                <label for="name">Name:</label>
                <input type="text" id="name" v-model="name" required />
            </div>
            <div>
                <label for="date">Date:</label>
                <input type="date" id="date" v-model="date" required />
            </div>
            <button type="submit">Check In</button>
        </form>
        <div v-if="message" class="message">{{ message }}</div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            name: '',
            date: '',
            message: ''
        };
    },
    methods: {
        handleCheckIn() {
            if (this.name && this.date) {
                this.message = `Checked in successfully for ${this.name} on ${this.date}`;
            } else {
                this.message = 'Please fill in all fields.';
            }
        }
    }
};
</script>

<style scoped>
.check-in {
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 8px;
}
.check-in h1 {
    text-align: center;
}
.check-in form {
    display: flex;
    flex-direction: column;
}
.check-in form div {
    margin-bottom: 10px;
}
.check-in form label {
    margin-bottom: 5px;
}
.check-in form input {
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 4px;
}
.check-in button {
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}
.check-in button:hover {
    background-color: #0056b3;
}
.message {
    margin-top: 20px;
    text-align: center;
    color: green;
}
</style>