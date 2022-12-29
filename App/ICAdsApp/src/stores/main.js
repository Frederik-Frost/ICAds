import { ref, computed, nextTick } from 'vue';
import { defineStore } from 'pinia';
import axios from 'axios';
import { useUserStore } from './user';
import { useOrgStore } from './organization';

export const useMainStore = defineStore({
  id: 'main',
  state: () => ({
    token: localStorage.getItem('userToken'),
  }),

  getters: () => ({}),

  actions: {
    login(credentials) {
      return new Promise((resolve, reject) => {
        axios
          .post('users/login', credentials)
          .then((response) => {
            console.log(response);
            localStorage.setItem('userToken', response.data);
            this.token = response.data;
            console.log(this.$router);
            this.$router.push('/');
            resolve(true);
          })
          .catch((e) => {
            console.log(e);
            reject(e.response.data.message);
          });
      });
    },
    
    signup(info){
      return axios.post('organization', info)
    },
    
    logout() {
      const resetStates = () => {
        const userStore = useUserStore();
        const orgStore = useOrgStore();
        orgStore.$reset();
        userStore.$reset();
        this.$reset();
        location.reload()
      }

      const goToLogin = (callback) => {
        localStorage.removeItem('userToken');
        this.token = null;
        this.$router.push('login');
        callback();
      }

      goToLogin(resetStates);
    },
  },
});
