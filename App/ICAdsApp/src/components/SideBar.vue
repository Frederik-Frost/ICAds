<template>
  <div id="sideBar" class="w-[65px] bg-charcoal h-screen transition-all">
    <router-link :to="{ name: 'home' }">
      <img src="./../assets/svg/adslogo.svg" alt="IC Ads logo" class="w-[35px] mx-[15px] my-[20px]" />
    </router-link>

    <router-link :to="{ name: 'home' }" class="menu-link group relative">
      <span class="material-symbols-outlined text-alabaster"> wallpaper </span>
      <span class="hover-menu">Layouts</span>
    </router-link>

    <router-link :to="{ name: 'export' }" class="menu-link group relative">
      <span class="material-symbols-outlined text-alabaster"> send </span>
      <span class="hover-menu">Export</span>
    </router-link>

    <router-link :to="{ name: 'account' }" class="menu-link  group relative">
      <span class="material-symbols-outlined text-alabaster"> people </span>
      <span class="hover-menu">Account</span>
    </router-link>
  </div>
</template>

<script>
import { computed, reactive, defineComponent, onMounted, ref } from 'vue';
export default defineComponent({
  setup() {},
});
</script>

<style></style>
