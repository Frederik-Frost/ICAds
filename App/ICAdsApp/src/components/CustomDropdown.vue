<template>
  <div class="dropdown relative">
    <div
      class="cursor-pointer "
      @click="show = !show"
    >
        <slot name="toggle"></slot>
    </div>
    <div
      class="absolute rounded shadow bg-white overflow-hidden w-full mt-1 z-50"
      v-if="show"
      @click="show = !show"
    >
      <slot name="content" />
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