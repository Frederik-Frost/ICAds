<template>
  <div class="dropdown relative" ref="componentRef">
    <div class="cursor-pointer" @click="show = !show">
      <slot name="toggle"></slot>
    </div>
    <div
      class="absolute rounded shadow bg-white overflow-hidden mt-1 z-50 w-full"
      :class="[right ? ['w-max', 'left-full', '-translate-x-full'] : '']"
      v-if="show"
      @click="show = !show"
    >
      <slot name="content" />
    </div>
  </div>
</template>
<script setup>
import { ref, defineProps } from 'vue';
import useDetectOutsideClick from './../assets/js/OutsideClickHandler';

const props = defineProps({
  title: String,
  right: Boolean,
});
const show = ref(false);

const componentRef = ref();
useDetectOutsideClick(componentRef, () => {
  show.value = false;
});
// export default {
//   props: ['title'],
//   setup(props) {
//     const show = ref(false);
//     return {
//       show,
//     };
//   },
// };
</script>
