import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import axios from 'axios';

export const useOrgStore = defineStore({
  id: 'organization',
  state: () => ({
    organization: null,
    layouts: [],
    integrations: [],
    layout: null,
    selectedProduct: null
  }),

  getters: () => ({}),

  actions: {
    getOrganization() {
      return new Promise(async (resolve, reject) => {
        if (this.organization) resolve(this.organization);
        else {
          axios
            .get('organization')
            .then((res) => {
              console.log(res);
              this.organization = res.data;
              resolve(this.organization);
            })
            .catch((e) => {
              console.log(e);
              reject(e);
            });
        }
      });
    },

    getLayouts() {
      return new Promise(async (resolve, reject) => {
        if (this.layouts.length > 0) resolve(this.layouts);
        else {
          axios
            .get('templates/metadata')
            .then((res) => {
              console.log(res);
              this.layouts = res.data;
              resolve(this.layouts);
            })
            .catch((e) => {
              console.log(e);
              reject(e);
            });
        }
      });
    },

    getLayout(id) {
      return new Promise((resolve, reject) => {
        axios.get(`templates/${id}`).then((res) => {
          console.log(res);
          this.layout = res.data;
          resolve(res);
        });
      });
    },

    createLayout(request) {
      return new Promise(async (resolve, reject) => {
        axios
          .post('templates', request)
          .then((res) => {
            console.log(res);
            this.layouts.push(res.data);
            resolve(res.data);
          })
          .catch((e) => {
            console.log(e);
            reject(e);
          });
      });
    },

    updateLayout(layout) {
      return new Promise(async (resolve, reject) => {
        axios
          .put(`templates/${layout.id}/metadata`, layout)
          .then((res) => {
            console.log(res);
            resolve(res.data);
          })
          .catch((e) => {
            console.log(e);
            reject(e);
          });
      });
    },

    deleteLayout(id) {
      return new Promise(async (resolve, reject) => {
        axios
          .delete(`templates/${id}`)
          .then((res) => {
            console.log(res);
            this.layouts.splice(
              this.layouts.findIndex((l) => l.id == id),
              1
            );
            resolve(res.data);
          })
          .catch((e) => {
            console.log(e);
            reject(e);
          });
      });
    },

    searchProduct(query) {
      return new Promise((resolve, reject) => {
        axios
          .get(`templates/${this.$router.currentRoute.value.params.layoutId}/products/search?query=${query}`)
          .then((res) => {
            console.log(res);
            resolve(res.data);
          });
      });
    },

    getProduct(id) {
      return new Promise((resolve, reject) => {
        axios.get(`templates/${this.$router.currentRoute.value.params.layoutId}/products/${id}`).then((res) => {
          console.log(res);
          this.selectedProduct = res.data.product;
          resolve(this.selectedProduct);
        });
      });
    },

    getIntegrations() {
      return new Promise(async (resolve, reject) => {
        axios
          .get('integrations')
          .then((res) => {
            console.log(res);
            this.integrations = res.data;
            resolve(this.integrations);
          })
          .catch((e) => {
            console.log(e);
            reject(e);
          });
      });
    },

    createIntegration(request) {
      return new Promise(async (resolve, reject) => {
        axios
          .post('integrations', request)
          .then((res) => {
            // console.log(res);
            // this.integrations.push(res.data);
            resolve(res.data);
          })
          .catch((e) => {
            console.log(e);
            reject(e);
          });
      });
    },

    updateIntegration(request) {
      return new Promise((resolve, reject) => {
        axios
          .put(`integrations/${request.id}`, request)
          .then((res) => {
            console.log(res);
            resolve(res.data);
          })
          .catch((e) => {
            console.log(e);
            reject(e);
          });
      });
    },
    deleteIntegration(id) {
      return new Promise((resolve, reject) => {
        axios
          .delete(`integrations/${id}`)
          .then((res) => {
            console.log(res);
            resolve(res.data);
          })
          .catch((e) => {
            console.log(e);
            reject(e);
          });
      });
    },
  },
});
