import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import LoginView from '../views/LoginView.vue';
import { useUserStore } from '../stores/user';
import { useOrgStore } from '../stores/organization';
import Template from './../assets/js/Template';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/export',
      name: 'export',
      component: () => import('../views/ExportView.vue'),
      beforeEnter: (from, to, next) => {
        const store = useOrgStore();
        store.getLayouts().then(() => next())
      }
    },
    {
      path: '/account',
      name: 'account',
      component: () => import('../views/AccountView.vue'),
    },
    {
      path: '/editor/:layoutId',
      name: 'editor',
      component: () => import('../views/EditorView.vue'),
      beforeEnter: (from, to, next) => {
        const store = useOrgStore();
        store.getLayout(from.params.layoutId).then((res) => {
          store.$patch((state) => state.layoutTemplate = new Template(res.data.template.templateJSON))
          next();
        })
      } 
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
  ],
});

export default router;
