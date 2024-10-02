<template>
<section id="team" class="team-area">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="site-heading text-center">
                        <h2>Our <span>Team</span></h2>
                        <h4>Meet our awesome and expert team members</h4>
                    </div>
                </div>
            </div>
                <div class="row team-items">
                    <div class="col-md-4 single-item">
                        <div class="item">
                            <div class="thumb">
                                <img class="img-fluid d-flex justify-content-center" src="../assets/AVTNgoc.png" alt="Thumb">
                                <div class="overlay">
                                    <h4>Harem</h4>
                                    <p>
                                        Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                    </p>
                                    <div class="social">
                                        <ul>
                                            <li class="twitter">
                                                <a href="#"><i class="fab fa-twitter"></i></a>
                                            </li>
                                            <li class="pinterest">
                                                <a href="#"><i class="fab fa-pinterest"></i></a>
                                            </li>
                                            <li class="instagram">
                                                <a href="#"><i class="fab fa-instagram"></i></a>
                                            </li>
                                            <li class="vimeo">
                                                <a href="#"><i class="fab fa-vimeo-v"></i></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="info">
                                <span class="message">
                                    <a href="#"><i class="fas fa-envelope-open"></i></a>
                                </span>
                                <h4>Nguyễn Văn Bảo Ngọc</h4>
                                <span>FullStack Dev</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 single-item">
                        <div class="item">
                            <div class="thumb">
                                <img class="img-fluid" src="../assets/AVTQuan.jpg" alt="Thumb">
                                <div class="overlay">
                                    <h4>Lorem Ipsum</h4>
                                    <p>
                                          kING.
                                    </p>
                                    <div class="social">
                                        <ul>
                                            <li class="twitter">
                                                <a href="#"><i class="fab fa-twitter"></i></a>
                                            </li>
                                            <li class="pinterest">
                                                <a href="#"><i class="fab fa-pinterest"></i></a>
                                            </li>
                                            <li class="instagram">
                                                <a href="#"><i class="fab fa-instagram"></i></a>
                                            </li>
                                            <li class="vimeo">
                                                <a href="#"><i class="fab fa-vimeo-v"></i></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="info">
                                <span class="message">
                                    <a href="#"><i class="fas fa-envelope-open"></i></a>
                                </span>
                                <h4>Ngô Trần Nguyên Quân</h4>
                                <span>Project-Manager</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 single-item">
                        <div class="item">
                            <div class="thumb">
                                <img class="img-fluid" src="../assets/AVTNhan.jpg" alt="Thumb">
                                <div class="overlay">
                                    <h4>Lorem Ipsum</h4>
                                    <p>
                                        Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                    </p>
                                    <div class="social">
                                        <ul>
                                            <li class="twitter">
                                                <a href="#"><i class="fab fa-twitter"></i></a>
                                            </li>
                                            <li class="pinterest">
                                                <a href="#"><i class="fab fa-pinterest"></i></a>
                                            </li>
                                            <li class="instagram">
                                                <a href="#"><i class="fab fa-instagram"></i></a>
                                            </li>
                                            <li class="vimeo">
                                                <a href="#"><i class="fab fa-vimeo-v"></i></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="info">
                                <span class="message">
                                    <a href="#"><i class="fas fa-envelope-open"></i></a>
                                </span>
                                <h4>Lê Nhật Nhân</h4>
                                <span>Web designer</span>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </section>
</template>
<style scoped>
body {
    Background-image: url(https://cdnb.artstation.com/p/assets/images/images/003/704/285/large/dusica-neskovic-muzejdanfinal.jpg?1476637309);
}
section {
  padding: 60px 0;
}
a, a:hover, a:focus, a:active {
  text-decoration: none;
  outline: none;
}
ul {
  margin: 0;
  padding: 0;
  list-style: none;
}

.site-heading h2 {
display: block;
font-weight: 700;
margin-bottom: 10px;
text-transform: uppercase;
}

.site-heading h2 span {
color: #ff5a6e;
}

.site-heading h4 {
display: inline-block;
padding-bottom: 20px;
position: relative;
text-transform: capitalize;
z-index: 1;
}

.site-heading h4::before {
background: #ff5a6e none repeat scroll 0 0;
bottom: 0;
content: "";
height: 2px;
left: 50%;
margin-left: -25px;
position: absolute;
width: 50px;
}

.site-heading h2 span {
color: #ff5a6e;
}

.site-heading {
margin-bottom: 60px;
overflow: hidden;
margin-top: -5px;
}

.team-area .single-item {
margin-bottom: 30px;
}

.team-area .item .thumb {
position: relative;
overflow: hidden;
z-index: 1;
}

.team-area .item .thumb::after {
background: #232323 none repeat scroll 0 0;
content: "";
height: 100%;
left: 0;
opacity: 0;
position: absolute;
top: 0;
transition: all 0.35s ease-in-out;
-webkit-transition: all 0.35s ease-in-out;
-moz-transition: all 0.35s ease-in-out;
-ms-transition: all 0.35s ease-in-out;
-o-transition: all 0.35s ease-in-out;
width: 100%;
}

.team-area .team-items .item:hover .thumb::after {
opacity: 0.7;
}

.team-area .item .thumb .overlay {
top: -100%;
left: 0;
padding: 20px;
position: absolute;
text-align: center;
-webkit-transition: all 0.35s ease-in-out;
-moz-transition: all 0.35s ease-in-out;
-ms-transition: all 0.35s ease-in-out;
-o-transition: all 0.35s ease-in-out;
transition: all 0.35s ease-in-out;
width: 100%;
z-index: 1;
}

.team-area .item:hover .thumb .overlay {
top: 50%;
transform: translate(-50%, -50%);
left: 50%;
}

.team-area .item .thumb .overlay p {
color: #ffffff;
}

.team-area .item .thumb .overlay h4 {
color: #ffffff;
display: inline-block;
position: relative;
text-transform: uppercase;
}

.team-area .item .thumb img {
-webkit-transition: all 0.35s ease-in-out;
-moz-transition: all 0.35s ease-in-out;
-ms-transition: all 0.35s ease-in-out;
-o-transition: all 0.35s ease-in-out;
transition: all 0.35s ease-in-out;
}

.team-area .item:hover .thumb img {
opacity: .6;
}

.team-area .item .thumb .social li {
display: inline-block;
}

.team-area .item .thumb .social li a {
-webkit-border-radius: 50%;
-moz-border-radius: 50%;
border-radius: 50%;
color: #ffffff;
display: inline-block;
height: 40px;
line-height: 40px;
margin: 0 2px;
text-align: center;
width: 40px;
}

.team-area .info {
background: #ffffff none repeat scroll 0 0;
-moz-box-shadow: 0 0 10px #cccccc;
-webkit-box-shadow: 0 0 10px #cccccc;
-o-box-shadow: 0 0 10px #cccccc;
box-shadow: 0 0 10px #cccccc;
padding: 40px 20px 20px;
position: relative;
text-align: center;
z-index: 9;
}

.team-area .info .message {
height: 50px;
line-height: 40px;
margin-left: -25px;
margin-top: -25px;
position: absolute;
text-align: center;
top: 0;
width: 50px;
}

.team-area .info .message a {
background: #fff none repeat scroll 0 0;
-webkit-border-radius: 50%;
-moz-border-radius: 50%;
border-radius: 50%;
-moz-box-shadow: 0 0 10px #cccccc;
-webkit-box-shadow: 0 0 10px #cccccc;
-o-box-shadow: 0 0 10px #cccccc;
box-shadow: 0 0 10px #cccccc;
box-sizing: border-box;
color: #ff5a6e;
display: inline-block;
font-size: 20px;
height: 50px;
line-height: 50px;
width: 50px;
}

.team-area .info .message a i {
font-weight: 500;
}

.team-area .info h4 {
font-weight: 600;
margin-bottom: 5px;
text-transform: capitalize;
}

.team-area .info span {
color: #ff5a6e;
font-family: "Poppins",sans-serif;
font-weight: 600;
text-transform: uppercase;
}

.team-area .social li.twitter a {
background-color: #00b6f1;
}

.team-area .social li.pinterest a {
background-color: #bd081c;
}

.team-area .social li.facebook a {
background-color: #3b5998;
}

.team-area .social li.google-plus a {
background-color: #df4a32;
}

.team-area .social li.vimeo a {
background-color: #1ab7ea;
}

.team-area .social li.instagram a {
background-color: #cd486b;
}
</style>