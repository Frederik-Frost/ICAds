<template>
  <div class="dropdown relative">
    <div
      class="cursor-pointer  flex rounded items-center p-3"
      @click="show = !show"
    >
        <slot name="toggle"></slot>
    </div>
    <div
      class="absolute rounded shadow bg-white overflow-hidden peer-checked:flex flex-col w-full mt-1 border border-gray-200"
      v-if="show"
    >
      <slot />
    </div>
  </div>
</template>
<script>
import { ref } from "vue";
export default {
  props: ["title"],
  setup(props) {
    const show = ref(false);
    return {
      show,
    };
  },
};
</script>