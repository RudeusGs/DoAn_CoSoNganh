<template>
    <div class="form-body">
        <div class="row justify-content-center align-items-center min-vh-100">
            <div class="form-holder col-lg-8 col-md-10 col-sm-12">
                <div class="form-content shadow-lg p-5 rounded">
                    <div class="form-items">
                        <h3>Register Today</h3>
                        <p>Fill in the data below.</p>
                        <form class="requires-validation" novalidate>
                            <div class="mb-3">
                                <input class="form-control" type="text" name="name" placeholder="Full Name" required>
                                <div class="valid-feedback">Username field is valid!</div>
                                <div class="invalid-feedback">Username field cannot be blank!</div>
                            </div>

                            <div class="mb-3">
                                <input class="form-control" type="email" name="email" placeholder="E-mail Address" required>
                                <div class="valid-feedback">Email field is valid!</div>
                                <div class="invalid-feedback">Email field cannot be blank!</div>
                            </div>

                            <div class="mb-3">
                                <select class="form-select" required>
                                    <option selected disabled value="">Position</option>
                                    <option value="jweb">Junior Web Developer</option>
                                    <option value="sweb">Senior Web Developer</option>
                                    <option value="pmanager">Project Manager</option>
                                </select>
                                <div class="valid-feedback">You selected a position!</div>
                                <div class="invalid-feedback">Please select a position!</div>
                            </div>

                            <div class="mb-3">
                                <input class="form-control" type="password" name="password" placeholder="Password" required>
                                <div class="valid-feedback">Password field is valid!</div>
                                <div class="invalid-feedback">Password field cannot be blank!</div>
                            </div>

                            <div class="mb-3">
                                <label class="mb-2 d-block">Gender:</label>

                                <div class="btn-group" role="group">
                                    <input type="radio" class="btn-check" name="gender" id="male" autocomplete="off" required>
                                    <label class="btn btn-outline-secondary" for="male">Male</label>

                                    <input type="radio" class="btn-check" name="gender" id="female" autocomplete="off" required>
                                    <label class="btn btn-outline-secondary" for="female">Female</label>

                                    <input type="radio" class="btn-check" name="gender" id="secret" autocomplete="off" required>
                                    <label class="btn btn-outline-secondary" for="secret">Secret</label>
                                </div>
                                <div class="valid-feedback mv-up">You selected a gender!</div>
                                <div class="invalid-feedback mv-up">Please select a gender!</div>
                            </div>

                            <div class="form-check mb-3">
                                <input class="form-check-input" type="checkbox" value="" id="invalidCheck" required>
                                <label class="form-check-label" for="invalidCheck">I confirm that all data are correct</label>
                                <div class="invalid-feedback">Please confirm that the entered data are correct!</div>
                            </div>

                            <div class="form-button">
                                <button id="submit" type="submit" class="btn btn-primary w-100">Register</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="js">
document.addEventListener('DOMContentLoaded', function () {
    'use strict'
    const forms = document.querySelectorAll('.requires-validation')

    Array.from(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;700;900&display=swap');

*,
body {
    font-family: 'Poppins', sans-serif;
    font-weight: 400;
    -webkit-font-smoothing: antialiased;
    text-rendering: optimizeLegibility;
    -moz-osx-font-smoothing: grayscale;
}

html,
body {
    height: 100%;
    background-color: #152733;
    overflow: hidden;
}

.form-holder {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    text-align: center;
    min-height: 100vh;
}

.form-content {
    background-color: #fff;
    padding: 50px;
    border-radius: 10px;
    text-align: left;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
    min-width: 720px;
}

.form-items {
    width: 100%;
}

h3 {
    color: #152733;
    font-size: 28px;
    font-weight: 600;
    margin-bottom: 20px;
}

p {
    color: #666;
    font-size: 16px;
    margin-bottom: 20px;
}

input[type="text"],
input[type="email"],
input[type="password"],
select {
    width: 100%;
    padding: 10px;
    margin-top: 10px;
    border-radius: 5px;
    border: 1px solid #ccc;
}

input[type="text"]:focus,
input[type="email"]:focus,
input[type="password"]:focus,
select:focus {
    border-color: #7f8080;
    box-shadow: 0 0 5px rgba(0, 128, 128, 0.2);
}

.btn-primary {
    background-color: #8b8e8e;
    border: none;
    padding: 10px;
    border-radius: 5px;
    font-weight: 500;
}

.btn-primary:hover {
    background-color: #676767;
}

.btn-check:focus + .btn,
.btn:focus {
    box-shadow: none;
}

.invalid-feedback {
    color: #ff606e;
}

.valid-feedback {
    color: #2acc80;
}
</style>
