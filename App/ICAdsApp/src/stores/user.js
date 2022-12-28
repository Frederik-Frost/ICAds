import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import axios from 'axios';

export const useUserStore = defineStore({
  id: 'user',
  state: () => ({
    user: null,
  }),

  getters: () => ({}),

  actions: {
    getUser() {
      return new Promise(async (resolve, reject) => {
        if (this.user) resolve(this.user);
        else {
          axios
            .get('users/me')
            .then((res) => {
              console.log(res);
              this.user = res.data;
              resolve(this.user);
            })
            .catch((e) => {
              console.log(e);
              reject(e);
            });
        }
      });
    },

    updateUserInfo(userData) {
      const updatedUser = {
        firstname: userData.firstname,
        lastname: userData.lastname,
        email: userData.email,
        id: userData.id,
      };
      axios.put('users/me', updatedUser).then(res => {
        this.user = res.data
      })
    },
  },
});
