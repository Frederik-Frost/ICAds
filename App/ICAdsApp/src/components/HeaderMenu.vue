<template>
  <header class="bg-white w-full h-[48px] flex shadow items-center px-6 justify-between">
    <h1 class="text-xl text-charcoal50">{{ title }}</h1>
    <main class="flex gap-4">
      <CustomDropdown title="test">
        <template v-slot:toggle>
          <div class="flex gap-2 text-charcoal">
            <span class="material-symbols-outlined"> people </span>
            <div v-if="orgStore.organization">{{ orgStore.organization.name }}</div>
            <span class="material-symbols-outlined"> expand_more </span>
          </div>
        </template>
        <div>
          <span>item 1</span>
          <span>item 2</span>
        </div>
      </CustomDropdown>
      <CustomDropdown title="test 2">
        <template v-slot:toggle>
          <div class="flex gap-2 text-charcoal">
            <span class="material-symbols-outlined"> person </span>
            <div v-if="user">{{ user.firstname }}</div>
            <span class="material-symbols-outlined"> expand_more </span>
          </div>
        </template>
        <div>
          <span>item 1</span>
          <span>item 2</span>
        </div>
      </CustomDropdown>
    </main>
  </header>
</template>

<script setup>
import { computed, reactive, defineComponent, onMounted, ref, defineProps } from 'vue';
import CustomDropdown from './CustomDropdown.vue';
import { useOrgStore } from './../stores/organization';
import { useUserStore } from './../stores/user';
const orgStore = useOrgStore();
const userStore = useUserStore();
const user = computed(() => userStore.user);
let org = ref('');

const props = defineProps({
  title: String,
});


</script>

<style></style>
