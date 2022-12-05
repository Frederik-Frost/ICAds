<template>
  <div class="flex flex-col gap-2">
    <div class="flex flex-col gap-2 border-b border-charcoal50 pb-2 mb-2">
      <div class="form-group">
        <label for="text">Text </label>
        <input type="text" name="text" v-model="layer.text" />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, defineProps, computed } from 'vue';
import fonts from './../assets/js/Fonts.json';
const props = defineProps({
  selectedLayerIndex: Number,
  layer: Object,
});

</script>

<style></style>
