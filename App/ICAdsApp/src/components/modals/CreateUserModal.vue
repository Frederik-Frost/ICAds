<template>
  <vue-final-modal
    :min-width="400"
    :max-width="700"
    name="NewUserModal"
    v-slot="{ close }"
    v-bind="$attrs"
    classes="flex justify-center items-center "
    content-class="relative flex flex-col max-h-full mx-4 p-4 rounded bg-white w-[400px] "
  >
    <button class="absolute top-0 right-0 mt-4 mr-4" @click="close">
      <span class="material-symbols-outlined"> close </span>
    </button>
    <h3 class="mr-8">
      <slot name="title"></slot>
    </h3>

    <form @submit.stop.prevent class="flex flex-col gap-y-2">
       
        <CustomInput type="text" name="email" label="Email" v-model="newUser.email" />
        <CustomInput type="text" name="firstname" label="First name" v-model="newUser.firstname" />
        <CustomInput type="text" name="lastname" label="Last name" v-model="newUser.lastname" />
        <CustomInput type="password" name="userPassword" label="Password" v-model="newUser.password" />

      <div class="flex justify-end gap-1 mt-2">
        <button type="submit" class="btn-primary" @click="$emit('confirm', close, newUser)">Create</button>
        <button type="cancel" class="btn-secondary" @click="$emit('cancel', close)">Cancel</button>
      </div>
    </form>
  </vue-final-modal>
</template>
<script setup>
import { onMounted, ref, defineProps } from '@vue/runtime-core';
import CustomInput from './../CustomInput.vue';
const newUser = ref({
    email: '',
    firstname: '',
    lastname: '',
    password:''
});
</script>
<script>
export default {
  inheritAttrs: false,
};
</script>
