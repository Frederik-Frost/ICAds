<template>
  <transition enter-active-class="duration-300 ease-out" enter-from-class="translate-y-[50px]">
    <div class="bg-white h-12 px-4 flex items-center justify-between shadow-sm">
      <p class="text-lg text-charcoal75">
        {{ orgStore.layout.name }}
      </p>

      <p class="">
        {{ orgStore.selectedProduct ? orgStore.selectedProduct.title : 'No product data loaded' }}
      </p>
    </div>
  </transition>
</template>

<script setup>
import { useOrgStore } from './../stores/organization';
const orgStore = useOrgStore();
</script>

<style></style>
