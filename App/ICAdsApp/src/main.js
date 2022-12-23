import { createApp, markRaw } from 'vue';
import { createPinia } from 'pinia';
import { vfmPlugin, $vfm } from 'vue-final-modal';

import App from './App.vue';
import router from './router';

// Axios config
import * as Vue from 'vue';
import axios from 'axios';
import VueAxios from 'vue-axios';

import './assets/main.css';
import './assets/index.css';

const app = createApp(App);

const pinia = createPinia();
pinia.use(({ store }) => {
  store.$router = markRaw(router);
});

app.use(pinia);
app.use(router);
app.use(VueAxios, axios);
app.use(vfmPlugin);

import { useMainStore } from './stores/main';
import { useUserStore } from './stores/user';
import { useOrgStore } from './stores/organization';

const store = useMainStore();
const userStore = useUserStore();
const orgStore = useOrgStore();

// Check for token authorization on each router navigation
router.beforeEach((to, from, next) => {
  if (store.token) {
    // CALLING GET USER
    Promise.all([userStore.getUser(), orgStore.getOrganization()]).then((values) => {
      next();
    });
  } else if (!['login'].includes(to.name)) next('/login');
  else next();
});

// HTTP Request settings
axios.defaults.baseURL = window.location.href.includes("127.0.0.1") ? 'https://localhost:7093/' : 'https://icads-api.azurewebsites.net';
// axios.defaults.baseURL = 'https://icads-api.azurewebsites.net'
// axios.defaults.baseURL = 'https://localhost:7093/'

console.log(axios.defaults.baseURL)
axios.interceptors.request.use(function (config) {
  if (!config.url.includes('login')) {
    // Set bearer token on all requests
    config.headers['Authorization'] = 'Bearer ' + store.token;
  }
  return config;
});

axios.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response.status == 401) {
      // if Unauthorized === no token or no valid token in request headers. go to login screen
      store.logout();
    } else return Promise.reject(error);
  }
);

app.mount('#app');
