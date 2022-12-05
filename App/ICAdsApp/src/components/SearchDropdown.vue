<template>
  <div class="relative" ref="componentRef">
    <div class="flex items-center">
      <span class="material-symbols-outlined"> search </span>
      <input
        type="text"
        class="border-none rounded-none border-b-2"
        placeholder="Search product data.."
        v-model="query"
        @input="searchDebounce()"
      />
    </div>
    <div class="bg-white rounded-lg shadow-md fixed top-10 z-50 min-w-[150px] max-w-[400px]" v-if="showResults">
      <div class="p-4" v-if="loading">Loading products...</div>
      <div class="p-4" v-else-if="results.length == 0">No products found on search '{{ query }}'</div>

      <div
        v-else
        v-for="(result, index) in results"
        :key="index"
        class="focus-within:bg-alabaster hover:bg-alabaster cursor-pointer px-4 py-1"
      >
        <button
          type="button"
          @click="selectResult(result.node)"
          class="text-left whitespace-nowrap overflow-hidden text-ellipsis max-w-full focus:outline-none"
        >
          {{ result.node.title }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import _debounce from 'lodash/debounce';
import { ref, defineProps, onMounted, defineEmits, watch } from 'vue';
import useDetectOutsideClick from './../assets/js/OutsideClickHandler';

const componentRef = ref();
useDetectOutsideClick(componentRef, () => {
  showResults.value = false;
});

const query = ref('');
const props = defineProps({
  searchModel: String,
  results: Array,
});
onMounted(() => {
  query.value = props.searchModel ? props.searchModel : '';
});

const showResults = ref(false);
const loading = ref(false);
const emit = defineEmits(['update', 'select']);
const searchDebounce = _debounce(() => {
  showResults.value = true;
  loading.value = true;
  emit('update', query.value);
}, 500);

watch(
  () => props.results,
  (first, second) => {
    loading.value = false;
  }
);

const selectResult = (node) => {
  emit('select', node);
  showResults.value = false;
};
</script>

<style></style>
