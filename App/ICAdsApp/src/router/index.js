import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import LoginView from '../views/LoginView.vue';
import { useUserStore } from '../stores/user';
import { useOrgStore } from '../stores/organization';

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
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/ExportView.vue'),
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
        console.log(from.params.layoutId)
        const store = useOrgStore();
        store.getLayout(from.params.layoutId).then((res) => {
          console.log(res);
          store.$patch((state) => state.layoutTemplate = res.data.template.templateJSON)
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
