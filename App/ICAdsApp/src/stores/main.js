import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import axios from 'axios';

export const useMainStore = defineStore({
  id: 'main',
  state: () => ({
    token: localStorage.getItem('userToken'),
    // user: 'Fred',
  }),

  getters: () => ({}),

  actions: {
    login(credentials) {
      return new Promise((resolve, reject) => {
        axios.post('users/login', credentials).then(response => {
            console.log(response)
            localStorage.setItem('userToken', response.data)
            this.token = response.data
            console.log(this.$router)
            this.$router.push("/")
            resolve(true);
          })
          .catch((e) => {
            console.log(e)
            reject(e.response.data.message);
          });
      });
    },

    // async login(credentials) {
    //   try {
    //     const response = await axios.post('users/login', credentials);
    //     console.log("Response here - ", response);
    //   } catch (e) {
    //     console.log("error here - ", e)
    //   }
    // },

    // login(credentials) {
    //   return new Promise(async (resolve, reject) => {
    //     axios
    //       .post('users/login', credentials)
    //       .then((res) => {
    //         console.log(res);
    //         if (res && res.status == 200) {
    //           localStorage.setItem('userToken', res.data);
    //           this.token = res.data;
    //           this.$router.push('/');
    //           resolve(true);
    //         } else {
    //           console.log('Wrong credentials 1');
    //           reject(res);
    //         }
    //       })
    //       .catch((e) => {
    //         console.log('Wrong credentials 2');
    //         reject(e);
    //       });
    //   });
    // },

    logout() {
      localStorage.removeItem('userToken');
      this.token = null;
      this.$router.push('login');
    },
  },
});
