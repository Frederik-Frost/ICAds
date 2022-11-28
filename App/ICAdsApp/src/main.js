import { createApp, markRaw } from 'vue';
import { createPinia } from 'pinia';
import { vfmPlugin , $vfm} from 'vue-final-modal'

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
app.use(vfmPlugin)

import { useMainStore } from './stores/main';
import { useUserStore } from './stores/user';
import { useOrgStore } from './stores/organization';

const store = useMainStore();
const userStore = useUserStore();
const orgStore = useOrgStore();

router.beforeEach((to, from, next) => {
  console.log(    store.token      )
  if (store.token) {
    console.log("CALLING GETUSER")
    Promise.all([userStore.getUser(), orgStore.getOrganization()]).then((values) => {
      console.log(values);
      next();
    });
    
    // userStore.getUser().then((res) => {
    //   next();
    // });
  } else if (!['login'].includes(to.name)) next('/login');
  else next();
});

axios.defaults.baseURL = 'https://localhost:7093/';
axios.interceptors.request.use(function (config) {

  console.log(router.currentRoute.value.name)
  // if(!["login"].includes(router.currentRoute.value.name)){
  if(!config.url.includes("login")){
    config.headers['Authorization'] = 'Bearer ' + store.token;
  }
  console.log(config);
  return config;
});

axios.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response.status == 401) {
      console.log('GO TO LOGIN');
      store.logout()
    } else return Promise.reject(error);
  }
);

app.mount('#app');
