<template>
  <div class="fixed w-screen h-screen bg-charcoal bg-opacity-50 top-0 left-0" @click.self="$emit('close')">
    <transition enter-active-class="duration-300 ease-out" enter-from-class="translate-x-[100px]">
      <aside class="w-[400px] h-screen bg-white absolute right-0 overflow-auto" v-if="showSidepanel">
        <slot name="content"></slot>
      </aside>
    </transition>
  </div>
</template>
<script setup>
import { onMounted, ref } from 'vue';
const showSidepanel = ref(false);
onMounted(() => {
  setTimeout(() => {
    showSidepanel.value = true;
  }, 50);
});
</script>

<style></style>
