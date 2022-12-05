<template>
  <header class="bg-white w-full h-[48px] flex shadow items-center px-2 justify-between gap-x-4 relative z-10">
    <div class="flex flex-row items-center gap-x-4">
      <transition enter-active-class="duration-300 ease-out" enter-from-class="-translate-x-[50px]">
        <div v-if="editor">
          <router-link :to="{ name: 'home' }">
            <img src="./../assets/svg/adslogo.svg" alt="IC Ads logo" class="w-[30px] brightness-0 opacity-50" />
          </router-link>
        </div>
      </transition>

      <h1 class="text-xl text-charcoal50">{{ title }}</h1>
    </div>
    <main class="flex flex-row justify-between items-center" :class="{ 'flex-1': editor }">
      <div v-if="editor" class="ml-4">
        <SearchDropdown @update="searchProduct" @select="getProductVariables" :results="products" />
      </div>

      <div class="flex gap-4">
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
      </div>
    </main>
  </header>
</template>

<script setup>
import { computed, reactive, defineComponent, onMounted, ref, defineProps } from 'vue';
import CustomDropdown from './CustomDropdown.vue';
import SearchDropdown from './SearchDropdown.vue';
import { useOrgStore } from './../stores/organization';
import { useUserStore } from './../stores/user';
const orgStore = useOrgStore();
const userStore = useUserStore();
const user = computed(() => userStore.user);
let org = ref('');

const props = defineProps({
  title: String,
  router: Object,
});

let products = ref([]);
const searchProduct = (query) => {
  console.log(query);
  orgStore.searchProduct(query).then((res) => {
    console.log(res);
    products.value = res.data.products.edges;
  });
};

const getProductVariables = (product) => {
  console.log(product);
  orgStore.$patch((state) => {
    state.selectedProduct = product
  });
  orgStore.getProductVariables(product.id).then(res => {
  
    console.log(res);
  });
};

const editor = computed(() => props.router.currentRoute.value.path.includes('editor'));
</script>

<style></style>
