<template>
  <vue-final-modal
    :min-width="400"
    :max-width="700"
    name="variableBrowserModal"
    v-slot="{ close }"
    v-bind="$attrs"
    classes="flex justify-center items-center "
    content-class="relative flex flex-col max-h-full mx-4  rounded bg-white "
  >
    <div class="flex flex-row justify-between p-4">
      <div class="flex flex-row gap-4 items-center">
        <h3 class="">
          <slot name="title"></slot>
        </h3>
        <p class="">
          <slot name="subtitle"></slot>
        </p>
      </div>

      <button class="flex justify-center items-center justify-self-end" @click="close">
        <span class="material-symbols-outlined"> close </span>
      </button>
    </div>
      <VariableBrowser :productVariables="variables" v-bind="$attrs" />
    
  </vue-final-modal>
</template>
<script setup>
import { onMounted, ref, defineProps } from '@vue/runtime-core';
import VariableBrowser from './../VariableBrowser.vue';
const name = ref('');
const props = defineProps({
  variables: Object,
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
