import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import axios from 'axios';
import EditorHelper from '../assets/js/EditorHelper';

export const useOrgStore = defineStore({
  id: 'organization',
  state: () => ({
    organization: null,
    organizationUsers: [],
    layouts: [],
    integrations: [],
    layout: null,
    selectedProduct: null,
    layoutTemplate: null,
    productVariables: null,
    layoutChanges: false,
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
    updateOrganizationInfo(orgInfo) {
      console.log(orgInfo);
      axios.put('organization', { name: orgInfo.name, id: orgInfo.id }).then((res) => {
        console.log(res);
        this.organization = res.data;
      });
    },
    getOrganizationUsers() {
      axios.get('organization/users').then((res) => {
        console.log(res);
        this.organizationUsers = res.data;
      });
    },
    createOrganizationUser(data) {
      return axios.post('organization/users', data).then((res) => {
        this.organizationUsers.push(res.data);
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

    searchProduct(query, layoutId=null) {
      return new Promise((resolve, reject) => {
        axios
          .get(`templates/${layoutId ? layoutId : this.$router.currentRoute.value.params.layoutId}/products/search?query=${query}`)
          .then((res) => {
            console.log(res);
            resolve(res.data);
          });
      });
    },

    getProduct(id, layoutId=null) {
      return new Promise((resolve, reject) => {
        axios.get(`templates/${layoutId ? layoutId : this.$router.currentRoute.value.params.layoutId}/products/${id}`).then((res) => {
          console.log(res);
          this.selectedProduct = res.data.product;
          resolve(this.selectedProduct);
        });
      });
    },

    getProductVariables(id, layoutId=null) {
      return new Promise((resolve, reject) => {
        axios
          .get(`templates/${layoutId ? layoutId : this.$router.currentRoute.value.params.layoutId}/products/${id}/variables`)
          .then((res) => {
            console.log(res);
            this.productVariables = res.data;
            resolve(res.data);
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

    generateImagePreview(template, variables=null) {
      return new Promise((resolve, reject) => {
        // axios.post('editor',{template: template, productData: this.selectedProduct}, {responseType: 'arraybuffer'}).then(res => {
        axios
          .post('editor/generate', { template: template, variables: variables ? variables : this.productVariables })
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

    testfind(template) {
      var url = axios.defaults.baseURL + 'editor';
      return fetch(url, {
        method: 'POST',
        body: { template: template, productData: this.selectedProduct },
        headers: {
          Accept: 'image/png',
          ResponseType: 'arraybuffer',
        },
      }).then((res) => res.blob());
    },

    saveChanges() {
      return new Promise((resolve, reject) => {
        // axios.post('editor',{template: template, productData: this.selectedProduct}, {responseType: 'arraybuffer'}).then(res => {
        axios
          .put(`templates/${this.$router.currentRoute.value.params.layoutId}/save`, this.layoutTemplate)
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
   
    layoutChanges() {
      this.layoutChanges = true;
    },


  // exportImageZip(template, variables){
  exportImageZip(data){
    console.log(data)
    // console.log(JSON.stringify(imageData))
  //  return axios.post('editor/export',  { template: template, variables: variables}).then(res => {
   return axios.post('editor/export',  data).then(res => {
      console.log(res)
      // EditorHelper.downloadZipFromBase64(res.data, "images")
      EditorHelper.jsDownloadZip(res.data, "images")
      
    })
  }
    
  },
});
