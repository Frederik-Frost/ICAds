<template>
  <vue-final-modal
    :min-width="400"
    :max-width="700"
    name="BaseModal"
    v-slot="{ params, close }"
    v-bind="$attrs"
    classes="flex justify-center items-center"
    content-class="relative flex flex-col max-h-full mx-4 p-4 rounded bg-white "
  >
    <button class="absolute top-0 right-0 mt-4 mr-4" @click="close">
      <span class="material-symbols-outlined"> close </span>
    </button>
    <h3 class="mr-8">
      <slot name="title"></slot>
    </h3>
    <p>
      <slot name="description"></slot>
    </p>
    <div class="flex-grow overflow-y-auto">
      <slot name="content" :params="params"></slot>
    </div>
    <div class="flex justify-end items-center gap-4 pt-4" v-if="!hideActions">
      <button class="btn-primary" @click="$emit('confirm', close)">confirm</button>
      <button class="btn-tertiary" @click="$emit('cancel', close)">cancel</button>
    </div>
  </vue-final-modal>
</template>
<script setup>
import { onMounted, ref, defineProps } from '@vue/runtime-core';
const name = ref('');
const props = defineProps({
  hideActions: Boolean,
});
onMounted(() => {
  console.log('first');
});
</script>
<script>
export default {
  inheritAttrs: false,
};
</script>
