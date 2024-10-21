<template>
  <div class="lucky-wheel-container">
    <div class="grid-wrapper">
      <!-- Loop through grid items (9 cells) -->
      <div
        v-for="(item, index) in gridItems"
        :key="index"
        :class="['grid-cell', { 'empty-cell': !item }]"
      >
        <div v-if="item && item.type === 'spin'">
          <button
            class="spin-button"
            :disabled="spinning"
            @click="spinWheel"
          >
            <i class="fas fa-redo-alt">Spin</i>
          </button>
        </div>
        <div
          v-else-if="item && item.type === 'prize'"
          :class="['prize-cell', { 'highlight': item.highlight, 'winning': item.data.id === selectedPrize?.id }]"
        >
          <img
            v-if="item.data.image"
            :src="getFullImageUrl(item.data.image)"
            alt="Prize Image"
            class="prize-image"
          />
          <span class="prize-name">{{ item.data.prize }}</span>
        </div>
        <div v-else class="empty-cell"></div>
      </div>
    </div>
    <div v-if="selectedPrize" class="result-modal">
      <div class="result-content">
        <h3>Congratulations!</h3>
        <p>You won: {{ selectedPrize.prize }}</p>
        <button @click="closeModal">Close</button>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, onMounted, computed } from 'vue';
import { luckyWheelApi } from '@/api/luckywheel.api'; // Adjust your import path

export default {
  name: 'LuckyWheel',
  setup() {
    const prizes = ref([]);
    const spinning = ref(false);
    const selectedPrize = ref(null);

    // Fetch prizes on component mount
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

    // Spin the wheel
    const spinWheel = async () => {
      if (spinning.value || prizes.value.length === 0) return;
      spinning.value = true;
      startSpinAnimation();
      try {
        const response = await luckyWheelApi.spin();
        if (response.isSuccess) {
          const prize = response.data;
          selectedPrize.value = prize;
          stopSpinAnimation(getPrizeIndex(prize.id));
        } else {
          alert(response.message || 'Spin failed.');
        }
      } catch (error) {
        console.error(error);
        alert('Spin error.');
      } finally {
        spinning.value = false;
      }
    };

    const closeModal = () => {
      selectedPrize.value = null;
    };

    // Build the full image URL
    const getFullImageUrl = (imageString) => {
      if (!imageString) return 'https://via.placeholder.com/336x198';
      const baseUrl = 'https://localhost:7224/';
      const firstImage = imageString.split(';')[0];
      return `${baseUrl}${firstImage}`;
    };

    const gridItems = computed(() => {
      const items = [];
      const totalCells = 9;
      const centerIndex = 4; // Center index for the spin button

      for (let i = 0; i < totalCells; i++) {
        if (i === centerIndex) {
          items.push({ type: 'spin', highlight: false });
        } else {
          const prizeIndex = i < centerIndex ? i : i - 1;
          const prize = prizes.value[prizeIndex];
          if (prize) {
            items.push({ type: 'prize', data: prize, highlight: false });
          } else {
            items.push(null); // Empty cell if no prize
          }
        }
      }
      return items;
    });

    const currentHighlightIndex = ref(0);
    let highlightInterval = null;

    // Start the spin animation
    const startSpinAnimation = () => {
      const totalCells = gridItems.value.length;
      currentHighlightIndex.value = 0;

      highlightInterval = setInterval(() => {
        const previousIndex = (currentHighlightIndex.value - 1 + totalCells) % totalCells;
        gridItems.value[previousIndex].highlight = false;
        gridItems.value[currentHighlightIndex.value].highlight = true;
        currentHighlightIndex.value = (currentHighlightIndex.value + 1) % totalCells;
      }, 100);
    };

    // Stop the spin animation at the prize index
    const stopSpinAnimation = (prizeIndex) => {
      clearInterval(highlightInterval);
      let counter = currentHighlightIndex.value;
      const totalSteps = 15; // Set this value to slow down toward the end
      const slowDown = setInterval(() => {
        counter = (counter + 1) % gridItems.value.length;
        gridItems.value.forEach((cell, index) => cell.highlight = index === counter);
        if (counter === prizeIndex) {
          clearInterval(slowDown);
        }
      }, 150); // Gradually increase the delay to slow down
    };

    // Function to get the index of the prize in the grid
    const getPrizeIndex = (prizeId) => {
      return gridItems.value.findIndex(item => item?.data?.id === prizeId);
    };

    return {
      prizes,
      spinning,
      selectedPrize,
      spinWheel,
      closeModal,
      getFullImageUrl,
      gridItems,
    };
  },
};
</script>

<style scoped>
.lucky-wheel-container {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  background-color: #2c3e50;
  min-height: 100vh;
  padding: 20px;
}

.grid-wrapper {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(3, 1fr);
  gap: 15px;
  background-color: #34495e;
  padding: 15px;
  border: 10px solid #f1c40f;
  border-radius: 15px;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.5);
  width: 400px;
  height: 400px;
}

.grid-cell {
  display: flex;
  justify-content: center;
  align-items: center;
}

.highlight {
  box-shadow: 0 0 20px 5px rgba(0, 255, 0, 0.7);
  transition: box-shadow 0.1s ease-in-out;
}

.prize-cell {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-color: #ecf0f1;
  border: 4px solid #bdc3c7;
  border-radius: 10px;
  width: 100px;
  height: 100px;
  padding: 10px;
  transition: transform 0.3s, background-color 0.3s, border-color 0.3s;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}

.prize-cell:hover {
  transform: scale(1.05);
  background-color: #bdc3c7;
}

.prize-cell.winning {
  background-color: #27ae60;
  border-color: #2ecc71;
  transform: scale(1.1);
  animation: pulse 1s infinite, glow 2s infinite;
}

.prize-image {
  width: 60px;
  height: 60px;
  object-fit: cover;
  margin-bottom: 5px;
  border-radius: 5px;
}

.prize-name {
  font-size: 16px;
  font-weight: bold;
  color: #2c3e50;
  text-align: center;
}

.spin-button {
  width: 80px;
  height: 80px;
  border-radius: 50%;
  border: none;
  background-color: #e74c3c;
  color: white;
  font-size: 24px;
  cursor: pointer;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
  transition: background-color 0.3s, transform 0.3s;
}

.spin-button:hover {
  background-color: #c0392b;
  transform: scale(1.1);
}

.spin-button:disabled {
  background-color: #95a5a6;
  cursor: not-allowed;
  transform: scale(1);
}

.result-modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.result-content {
  background-color: #ecf0f1;
  padding: 30px;
  border-radius: 10px;
  text-align: center;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.5);
}

.result-content h3 {
  margin-bottom: 20px;
  color: #27ae60;
}

.result-content p {
  font-size: 18px;
  margin-bottom: 30px;
  color: #2c3e50;
}

.result-content button {
  padding: 10px 20px;
  background-color: #2980b9;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s;
}

.result-content button:hover {
  background-color: #1c5980;
}
</style>
