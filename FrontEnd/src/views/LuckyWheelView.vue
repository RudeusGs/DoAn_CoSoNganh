<template>
        <div class="rule">
            <div class="rule__content" v-for="(rule, index) in rules" :key="index">
                <div :class="['rule__color', `color-${index + 1}`]"></div>
                <div class="rule__text">
                    {{ rule.text }}
                </div>
            </div>
        </div>
        <div class="wheel">
            <div class="wheel__inner">
                <div class="wheel__sec" v-for="(section, index) in 6" :key="index" :style="{ transform: `rotate(${index * 60}deg)` }"></div>
            </div>
            <div class="wheel__arrow">
                <button class="wheel__button">QUAY</button>
            </div>
        </div>
        <div class="popup">
            <div class="popup__container">
                <div class="popup__emotion">
                    <i class="fas fa-meh"></i>
                </div>
                <p class="popup__note"></p>
            </div>
        </div>
        <div class="congratulation">
            <div class="congratulation__container">
                <div class="congratulation__close">
                    <i class="fas fa-times"></i>
                </div>
                <div class="congratulation__emotion">
                    <i class="fas fa-smile"></i>
                </div>
                <p class="congratulation__note">CHÚC MỪNG BẠN ĐÃ TRÚNG 1 NHÀ LẦU</p>
            </div>
        </div> 
</template>
<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            value: 0,
            countClicked: 0,
            clicked: false,
            rules: [
                { text: '1 CĂN NHÀ LẦU 4 TẦNG' },
                { text: '1 CHUYẾN DU LỊCH MIỀN TÂY' },
                { text: '1 THẺ CÀO 100K' },
                { text: '1 THẺ CÀO 200K' },
                { text: 'CHÚC BẠN MAY MẮN LẦN SAU' },
                { text: '1 CHUYẾN DU LỊCH VŨNG TÀU' }
            ]
        };
    },
    methods: {
        getPosition(position: number) {
            let message: string;
            if (position <= 30) {
                message = 'CHÚC MỪNG BẠN TRÚNG ĐƯỢC MỘT NHÀ LẦU 4 TẦNG';
            } else if (position <= 90) {
                message = 'CHÚC MỪNG BẠN TRÚNG ĐƯỢC MỘT CHUYẾN DU LỊCH VŨNG TÀU';
            } else if (position <= 150) {
                message = 'CHÚC BẠN MAY MẮN LẦN SAU';
            } else if (position <= 210) {
                message = 'CHÚC MỪNG BẠN TRÚNG ĐƯỢC MỘT THẺ CÀO 200K';
            } else if (position <= 270) {
                message = 'CHÚC MỪNG BẠN TRÚNG ĐƯỢC MỘT THẺ CÀO 100K';
            } else if (position <= 330) {
                message = 'CHÚC MỪNG BẠN TRÚNG ĐƯỢC MỘT CHUYẾN DU LỊCH MIỀN TÂY';
            } else {
                message = 'CHÚC MỪNG BẠN TRÚNG ĐƯỢC MỘT CĂN NHÀ LẦU 4 TẦNG';
            }

            this.$nextTick(() => {
                const noteElement = document.querySelector('.congratulation__note') as HTMLElement;
                if (noteElement) {
                    noteElement.textContent = message;
                }

                const popupElement = document.querySelector('.popup') as HTMLElement;
                if (popupElement) {
                    popupElement.classList.remove('active');
                }

                const congratulationElement = document.querySelector('.congratulation') as HTMLElement;
                if (congratulationElement) {
                    congratulationElement.style.display = 'block';
                }
            });

            this.clicked = false;
            this.countClicked = 0;
        },

        wheelClick() {
            if (this.clicked) {
                this.countClicked++;
                let message = '';
                if (this.countClicked <= 2) {
                    message = "NGỪNG PHÁ ĐI MEN!";
                } else if (this.countClicked <= 4) {
                    message = "LÌ QUÁ NGHEN!";
                } else {
                    message = "BÓ TAY, RÁNG PHÁ BANH NÚT NHA!";
                }

                const popupNoteElement = document.querySelector('.popup__note') as HTMLElement;
                if (popupNoteElement) {
                    popupNoteElement.textContent = message;
                }

                const popupElement = document.querySelector('.popup') as HTMLElement;
                if (popupElement && !popupElement.classList.contains('active')) {
                    popupElement.classList.add('active');
                }
            } else {
                const random = Math.floor((Math.random() * 360) + 720);
                this.value += random;
                const wheelInnerElement = document.querySelector('.wheel__inner') as HTMLElement;
                if (wheelInnerElement) {
                    wheelInnerElement.style.transform = `rotate(${this.value}deg)`;
                }

                setTimeout(() => {
                    this.getPosition(this.value % 360);
                }, 5000);
            }

            this.clicked = true;
        },

        closeCongratulation() {
            const congratulationElement = document.querySelector('.congratulation') as HTMLElement;
            if (congratulationElement) {
                congratulationElement.style.display = 'none';
            }
        }
    },
    mounted() {
        const wheelButton = document.querySelector('.wheel__button') as HTMLElement;
        if (wheelButton) {
            wheelButton.addEventListener('click', this.wheelClick);
        }

        const closeButton = document.querySelector('.congratulation__close') as HTMLElement;
        if (closeButton) {
            closeButton.addEventListener('click', this.closeCongratulation);
        }

        const congratulationElement = document.querySelector('.congratulation') as HTMLElement;
        if (congratulationElement) {
            congratulationElement.addEventListener('click', (event) => {
                if (event.target !== congratulationElement) {
                    return;
                }
                this.closeCongratulation();
            });
        }
    }
});
</script>
<style scoped>
@import url("https://fonts.googleapis.com/css?family=Open+Sans:600,300");

*,
*::after,
*::before {
    outline: none;
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

html,
body {
    width: 100%;
    height: 100%;
}

body {
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: lightgray;
    font-family: 'Open Sans', sans-serif;
}

.rule {
    margin: 0 50px 0 0;
}

.rule__content {
    display: flex;
}

.rule__color {
    width: 50px;
    height: 50px;
    margin: 0 20px 20px 0;
}

.color-1 {
    background-color: #16a085;
}

.color-2 {
    background-color: #2980b9;
}

.color-3 {
    background-color: #34495e;
}

.color-4 {
    background-color: #f39c12;
}

.color-5 {
    background-color: #d35400;
}

.color-6 {
    background-color: #c0392b;
}

.wheel {
    width: 312px;
    height: 312px;
    border-radius: 50%;
    border: solid 6px #fff;
    box-shadow: 0 4px 9px 0 rgba(0, 0, 0, 0.1);
    position: relative;
}

.wheel__inner {
    width: 300px;
    height: 300px;
    border-radius: 50%;
    position: relative;
    overflow: hidden;
    transition: cubic-bezier(0.19, 1, 0.22, 1) 5s;
}

.wheel__sec {
    position: absolute;
    top: 0;
    left: 62px;
    width: 0;
    height: 0;
    border: solid;
    border-width: 150px 88px 0;
    border-color: transparent;
    transform-origin: 50% 100%;
}

.wheel__sec:nth-child(1) {
    border-top-color: #16a085;
}

.wheel__sec:nth-child(2) {
    border-top-color: #2980b9;
    transform: rotate(60deg);
}

.wheel__sec:nth-child(3) {
    border-top-color: #34495e;
    transform: rotate(120deg);
}

.wheel__sec:nth-child(4) {
    border-top-color: #f39c12;
    transform: rotate(180deg);
}

.wheel__sec:nth-child(5) {
    border-top-color: #d35400;
    transform: rotate(240deg);
}

.wheel__sec:nth-child(6) {
    border-top-color: #c0392b;
    transform: rotate(300deg);
}

.wheel__text {
    margin: 70px 0 0 70px;
}

.wheel__arrow {
    width: 70px;
    height: 70px;
    background: #fff;
    position: absolute;
    top: 121px;
    left: 115px;
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
    font-family: 'Open Sans', sans-serif;
}

.wheel__arrow::after {
    content: '';
    position: absolute;
    width: 0;
    height: 0;
    border: solid;
    border-width: 0 10px 20px;
    border-color: transparent;
    border-bottom-color: #fff;
    top: -15px;
    left: 25px;
}

.wheel__button {
    width: 60px;
    height: 60px;
    background-color: lightgray;
    border: none;
    border-radius: 50%;
    cursor: pointer;
    transition: 0.3s;
    font-weight: 600;
    font-size: 18px;
}

.wheel__button:hover {
    color: #27AE60;
}

.wheel__button:active {
    border: solid 3px rgba(0, 0, 0, 0.1);
    font-size: 15px;
}

.popup {
    position: fixed;
    width: 30vw;
    top: 0;
    left: 50%;
    transform: translate(-50%, -110%);
    background: #fff;
    box-shadow: 0 4px 9px 0 rgba(0, 0, 0, 0.1);
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 30px 20px;
    transition: all 0.5s;
    opacity: 0;
}

.popup.active {
    transform: translate(-50%, 0);
    opacity: 1;
}

.popup__emotion {
    color: #f39c12;
    text-align: center;
    font-size: 30px;
    margin: 0 0 25px 0;
}

.popup__note {
    text-align: center;
}

.congratulation {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background: rgba(0, 0, 0, 0.2);
    display: none;
}

.congratulation__container {
    width: 40vw;
    padding: 30px;
    background-color: #fff;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}

.congratulation__close {
    position: absolute;
    top: 10px;
    right: 10px;
    color: #c0392b;
    cursor: pointer;
    transition: all 0.5s;
}

.congratulation__close:hover {
    transform: rotate(360deg);
}

.congratulation__emotion {
    color: #f39c12;
    text-align: center;
    margin: 0 0 20px 0;
}

.congratulation__note {
    text-align: center;
}

</style>