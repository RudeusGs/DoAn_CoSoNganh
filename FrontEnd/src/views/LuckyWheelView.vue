<template>
  <div class="lucky-wheel-container">
    <h3>Vòng quay may mắn</h3>
    <div class="grid-wrapper">   
      <div
        v-for="(item, index) in gridItems"
        :key="index"
        :class="['grid-cell', { 'empty-cell': !item }]">
        <div v-if="item && item.type === 'spin'">
          <button class="spin-button" :disabled="spinning" @click="spinWheel">
            <i class="fas fa-redo-alt">Spin</i>
          </button>
        </div>
        <div
          v-else-if="item && item.type === 'prize'"
          :class="[
            'prize-cell',
            { 
              'highlight': index === currentHighlightIndex,
              'winning': !spinning && item.data.id === selectedPrize?.id 
            }
          ]">
          <img
            v-if="item.data.image"
            :src="getFullImageUrl(item.data.image)"
            alt="Prize Image"
            class="prize-image"/>
          <img
            v-else
            src="https://via.placeholder.com/336x198"
            alt="No Image Available"
            class="prize-image"/>

          <span class="prize-name">{{ item.data.prize }}</span>
        </div>
        <div v-else class="empty-cell"></div>
      </div>
    </div>
    <div v-if="showModal" class="result-modal">
      <div class="result-content">
        <h3>Congratulations!</h3>
        <p>You won: {{ selectedPrize.prize }}</p>
        <button @click="closeModal">Close</button>
      </div>
    </div>
    <audio ref="spinSound" :src="spinSoundSrc"></audio>
    <audio ref="winSound" :src="winSoundSrc"></audio>
  </div>
</template>

<script>
import { ref, onMounted, computed } from 'vue';
import { luckyWheelApi } from '@/api/luckywheel.api'; 


export default {
  name: 'LuckyWheel',
  setup() {
    const prizes = ref([]);
    const spinning = ref(false);
    const selectedPrize = ref(null);
    const showModal = ref(false);
    const spinSound = ref(null);
    const winSound = ref(null);

    const currentHighlightIndex = ref(null);

    const clockwiseSequence = [0, 1, 2, 5, 8, 7, 6, 3];

    onMounted(async () => {
      try {
        const response = await luckyWheelApi.getAllLuckyWheel();
        if (response && response.isSuccess) {
          prizes.value = response.data;
        } else {
          alert(response.message || 'Error fetching prizes.');
        }
      } catch (error) {
        console.error(error);
        alert('Error fetching prizes.');
      }
    });

    const gridItems = computed(() => {
      const items = [];
      const totalCells = 9;
      const centerIndex = 4;

      for (let i = 0; i < totalCells; i++) {
        if (i === centerIndex) {
          items.push({ type: 'spin', highlight: false });
        } else {
          const prizeIndex = i < centerIndex ? i : i - 1;
          const prize = prizes.value[prizeIndex];
          if (prize) {
            items.push({ type: 'prize', data: prize, highlight: false });
          } else {
            items.push(null);
          }
        }
      }
      return items;
    });

    let highlightInterval = null;

    const spinWheel = async () => {
      if (spinning.value || prizes.value.length === 0) return;
      spinning.value = true;

      if (spinSound.value) {
        spinSound.value.currentTime = 0;
        spinSound.value.play();
      }

      const spinButton = document.querySelector('.spin-button');
      spinButton.classList.add('spinning');

      try {
        const response = await luckyWheelApi.spin();
        if (response.isSuccess) {
          const prize = response.data;
          selectedPrize.value = prize;
          const prizeIndex = getPrizeIndex(prize.id);
          startSpinAnimation(prizeIndex);
        } else {
          alert(response.message || 'Spin failed.');
          spinning.value = false;
          spinButton.classList.remove('spinning');
        }
      } catch (error) {
        console.error(error);
        alert('Spin error.');
        spinning.value = false;
        spinButton.classList.remove('spinning');
      }
    };

    
    const startSpinAnimation = (prizeIndex) => {
      let accelerationPhase = true;
      let decelerationPhase = false;
      const initialSpeed = 30; 
      const maxSpeed = 500;
      let currentSpeed = initialSpeed;
      let lastTime = null;
      let accumulatedTime = 0;
      let spinCount = 0;
      const accelerationFrames = 15;
      const decelerationFrames = 60;

      const animate = (timestamp) => {
        if (!lastTime) lastTime = timestamp;
        const delta = timestamp - lastTime;
        lastTime = timestamp;
        accumulatedTime += delta;

        if (accumulatedTime >= currentSpeed) {
          currentHighlightIndex.value = clockwiseSequence[spinCount % clockwiseSequence.length];

          spinCount++;

          if (accelerationPhase && spinCount <= accelerationFrames) {
            currentSpeed = Math.max(initialSpeed, currentSpeed - 2)
            if (spinCount === accelerationFrames) {
              accelerationPhase = false;
              decelerationPhase = true;
            }
          }

          if (decelerationPhase && spinCount > accelerationFrames) {
            currentSpeed = Math.min(maxSpeed, currentSpeed + 2);
            if (spinCount >= (accelerationFrames + decelerationFrames)) {
              if (currentHighlightIndex.value === prizeIndex) {
                currentHighlightIndex.value = prizeIndex;
                endSpinAnimation();
                return;
              }
            }
          }

          accumulatedTime = 0;
        }

        highlightInterval = requestAnimationFrame(animate);
      };

      highlightInterval = requestAnimationFrame(animate);
    };

    const endSpinAnimation = () => {
      if (highlightInterval) {
        cancelAnimationFrame(highlightInterval);
        highlightInterval = null;
      }

      if (winSound.value && selectedPrize.value) {
        winSound.value.currentTime = 0;
        winSound.value.play();
      }
      showModal.value = true;
      const spinButton = document.querySelector('.spin-button');
      spinButton.classList.remove('spinning');

      spinning.value = false;
    };

    const closeModal = () => {
      showModal.value = false;
      selectedPrize.value = null;
    };

    const getFullImageUrl = (imageString) => {
  if (!imageString) return 'https://via.placeholder.com/336x198';
  const baseUrl = 'https://localhost:7224/';
  const fullUrl = `${baseUrl}${imageString}`;
  return fullUrl;
};




    const getPrizeIndex = (prizeId) => {
      return gridItems.value.findIndex(item => item?.data?.id === prizeId);
    };

    return {
      prizes,
      spinning,
      selectedPrize,
      showModal,
      spinWheel,
      closeModal,
      getFullImageUrl,
      gridItems,
      spinSound,
      winSound,
      currentHighlightIndex,
    };
  },
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap');
.highlight {
  box-shadow: 0 0 40px 15px rgba(255, 165, 0, 0.9);
  transition: box-shadow 0.4s ease-in-out, transform 0.4s ease-in-out;
  transform: scale(1.3);
  animation: spinHighlight 0.1s linear infinite;
}

@keyframes spinHighlight {
  0% { transform: scale(1); }
  25% { transform: scale(1.1); }
  50% { transform: scale(1.2); }
  75% { transform: scale(1.3); }
  100% { transform: scale(1); }
}

.spin-button.spinning {
  animation: rotate 1s infinite linear;
  transform-origin: center;
}

@keyframes rotate {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

.lucky-wheel-container {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  background: linear-gradient(135deg, #fbc2eb, #a6c1ee);
  min-height: 100vh;
  padding: 20px;
  font-family: 'Montserrat', sans-serif;
}

h3 {
  font-size: 36px;
  color: #ed1010;
  margin-bottom: 20px;
  text-shadow: 2px 2px 5px rgba(0, 0, 0, 0.3);
}

.grid-wrapper {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(3, 1fr);
  gap: 15px;
  padding: 15px;
  border-radius: 15px;
  background: rgba(255, 255, 255, 0.2);
  box-shadow: 0 0 25px rgba(0, 0, 0, 0.4);
  width: 400px;
  height: 400px;
}

.grid-cell {
  display: flex;
  justify-content: center;
  align-items: center;
}

.prize-cell {
  background: rgba(255, 255, 255, 0.2);
  border: 2px solid rgba(255, 255, 255, 0.3);
  border-radius: 10px;
  width: 100px;
  height: 100px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  transition: all 0.4s ease;
  position: relative;
  backdrop-filter: blur(5px);
}

.prize-cell:hover {
  transform: scale(1.1);
  background: rgba(255, 255, 255, 0.4);
  border-color: #fff;
}

.prize-cell.winning {
  background: linear-gradient(135deg, #ff4500, #ff6347);
  border-color: #ff6347;
  animation: fadeBlink 1s infinite ease-in-out;
}
@keyframes fadeBlink {
  0% {
    opacity: 1;
  }
  50% {
    opacity: 0.5;
  }
  100% {
    opacity: 1;
  }
}

@keyframes pulse {
  0% { box-shadow: 0 0 5px rgba(255, 127, 80, 0.8); }
  50% { box-shadow: 0 0 25px rgba(255, 127, 80, 1); }
  100% { box-shadow: 0 0 5px rgba(255, 127, 80, 0.8); }
}

.prize-image {
  width: 60px;
  height: 60px;
  object-fit: cover;
  margin-bottom: 10px;
  border-radius: 5px;
}

.prize-name {
  font-size: 14px;
  font-weight: bold;
  color: #3a3a3a;

  text-align: center;
  text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.5);
}

.spin-button {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  border: none;
  background: linear-gradient(135deg, #6a85b6, #bac8e0);
  color: white;
  font-size: 24px;
  cursor: pointer;
  box-shadow: 0 0 20px rgba(116, 193, 227, 0.7);
  transition: all 0.3s ease;
}

.spin-button:hover {
  transform: scale(1.1);
  background: linear-gradient(135deg, #596a9f, #a3b4d3);
}

.result-modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  backdrop-filter: blur(10px);
  z-index: 1000;
  animation: fadeIn 0.5s ease-in-out;
}

@keyframes fadeIn {
  from { opacity: 0; }
  to { opacity: 1; }
}

.result-content {
  background: #fff5ee;
  padding: 40px;
  border-radius: 15px;
  text-align: center;
  box-shadow: 0 0 30px rgba(255, 69, 0, 0.5);
  animation: popIn 0.5s ease;
}

.result-content h3 {
  font-size: 28px;
  color: #ff4500;
  margin-bottom: 20px;
  animation: zoomIn 0.5s ease;
}

.result-content p {
  font-size: 20px;
  color: #ff6347;
  margin-bottom: 30px;
}

.result-content button {
  padding: 15px 30px;
  background-color: #ff4500;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-size: 16px;
  transition: all 0.3s ease;
}

.result-content button:hover {
  background-color: #ff6347;
}

@keyframes popIn {
  from { transform: scale(0); }
  to { transform: scale(1); }
}

@keyframes zoomIn {
  from { transform: scale(0.5); opacity: 0; }
  to { transform: scale(1); opacity: 1; }
}
</style>

