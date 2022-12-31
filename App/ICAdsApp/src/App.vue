<script setup>
import { RouterLink, RouterView } from 'vue-router';
import SideBar from './components/SideBar.vue';
import HeaderMenu from './components/HeaderMenu.vue';
// import BaseModal from './components/modals/BaseModal.vue'
// const $vfm = inject('$vfm')
</script>

<template>
  <div id="app" class="flex">
    <SideBar v-if="showSideBar" />
    <div class="flex-1">
      <HeaderMenu v-if="showHeaderMenu" :title="headerTitle" :router="$router" />
      <RouterView />
    </div>

    <!-- <BaseModal/> -->
    <modals-container></modals-container>
  </div>
</template>
<script>
import { ModalsContainer } from 'vue-final-modal';
export default {
  components: { SideBar, HeaderMenu, ModalsContainer },
  methods: {
    // getList() {
    //   this.axios.get('users/all').then((response) => {
    //     console.log(response.data);
    //   });
    // },
  },
  computed: {
    headerTitle() {
      const path = this.$router.currentRoute.value.path;
      const pathWithParams = path.substring(0, path.lastIndexOf('/'));
      switch (pathWithParams.length > 0 ? pathWithParams : this.$router.currentRoute.value.path) {
        case '/':
          return 'Image layout';
        case '/export':
          return 'Export Creatives';
        case '/account':
          return 'Account';
        case '/editor':
          return 'Layout editor';
      }
    },
    showSideBar() {
      return (
        !['/login'].includes(this.$router.currentRoute.value.path) &&
        !this.$router.currentRoute.value.path.includes('editor')
      );
    },
    showHeaderMenu() {
      return !['/login'].includes(this.$router.currentRoute.value.path);
    },
    path(){
      return this.$router.currentRoute.value.path
    }
  },
  mounted() {
    // this.getList();
  },
};
</script>
<style>
*,
*::before,
*::after {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
  position: relative;
  font-weight: normal;
}

@font-face {
  font-family: '3954';
  src: url('./fonts/futura/3954.eot');
  src: local('3954'), url('./fonts/futura/3954.woff') format('woff'), url('./fonts/futura/3954.ttf') format('truetype');
}

h1,
h2,
h3,
h4 {
  font-family: futura-pt, sans-serif;
  font-family: '3954';
  text-transform: uppercase;
}
</style>
