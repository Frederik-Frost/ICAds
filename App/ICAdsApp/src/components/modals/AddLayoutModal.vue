<template>
  <vue-final-modal
    name="AddLayoutModal"
    v-slot="{ close }"
    v-bind="$attrs"
    classes="flex justify-center items-center"
    content-class="relative flex flex-col max-h-full mx-4 p-4 rounded bg-white w-[400px]"
  >
    <button class="absolute top-0 right-0 mt-4 mr-4" @click="close">
      <span class="material-symbols-outlined"> close </span>
    </button>
    <h3 class="mr-8">
      <slot name="title"></slot>
    </h3>

    <form @submit.stop.prevent class="flex flex-col gap-y-2">
      <label for="name">Name your new layout</label>
      <input ref="nameinput" type="text" name="name" v-model="name" />
      <IntegrationSelect
        :modelValue="integrationId"
        @updateValue="(id) => {integrationId = id}"
        :name="'CreateLayoutIntegration'"
        :id="'CreateLayoutIntegration'"
      />

      <div class="flex justify-end gap-1 mt-2">
        <button type="submit" class="btn-primary" @click="$emit('confirm', close, {name: name, integrationId: integrationId})">confirm</button>
        <button type="cancel" class="btn-secondary" @click="$emit('cancel', close)">cancel</button>
      </div>
    </form>
  </vue-final-modal>
</template>
<script setup>
import IntegrationSelect from './../IntegrationSelect.vue';
import { onMounted, ref, watchEffect } from '@vue/runtime-core';
const name = ref('');
const integrationId = ref('');
const nameinput = ref(null);
onMounted(() => {
  console.log('first');
  nameinput.value.focus();
});
</script>
<script>
export default {
  inheritAttrs: false,
};
</script>
