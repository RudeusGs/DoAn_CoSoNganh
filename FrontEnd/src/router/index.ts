import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import AboutView from '@/views/AboutView.vue';
import LoginView from '@/views/LoginView.vue';
import RegisterView from '@/views/RegisterView.vue';
import OurTeamView from '@/views/OurTeamView.vue';
import PhoneCardView from '@/views/PhoneCardView.vue';
import LuckyWheelView from '@/views/LuckyWheelView.vue';
import AddAccountView from '@/views/AddAccountView.vue';
import AuctionView from '@/views/AuctionView.vue';
import GameAccountDetailView from '@/views/GameAccountDetail.vue';
import { userStore } from '../stores/auth';

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    component: AboutView
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView
  },
  {
    path: '/ourteam',
    name: 'ourteam',
    component: OurTeamView
  },
  {
    path: '/phonecard',
    name: 'phonecard',
    component: PhoneCardView
  },
  {
    path: '/luckywheel',
    name: 'luckywheel',
    component: LuckyWheelView
  },
  {
    path: '/addaccount',
    name: 'addaccount',
    component: AddAccountView
  },
  {
    path: '/auction',
    name: 'auction',
    component: AuctionView
  },
  {
    path: '/gameaccountdetail/:id?',
    name: 'gameaccountdetail',
    component: GameAccountDetailView
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  const authStore = userStore();
  
  if (to.meta.requiresAuth && !authStore.user) {
    next('/login');
    alert("Không vào được đâu!");
  } else {
    next();
  }
});

export default router;
