<template>
  <div class="bg-white rounded-b-lg shadow-lg">
    <header class="flex flex-row gap-4 items-center bg-primary2half p-4 rounded-t-lg shadow-md justify-between">
      <h3 class="text-white">Layer </h3>
      <p class="text-white">{{ props.selectedLayerIndex != null ? props.selectedLayerIndex + 1 : 'None selected' }}</p>
    </header>
    <div v-if="layer">
      <div class="bg-white p-2 rounded shadow">
        <TextLayerEditor v-if="layer.layerType == 'TextLayer'" :layer="layer" class="flex flex-col gap-2" />
        <ImageLayerEditor v-if="layer.layerType == 'ImageLayer'" :layer="layer" class="flex flex-col gap-2" />
        <ShapeLayerEditor v-if="layer.layerType == 'ShapeLayer'" :layer="layer" class="flex flex-col gap-2" />
      </div>
    </div>
    <div v-else class="bg-white p-2 text-center mt-12">
      <p class="font-bold text-primary2half">Select a layer</p>
    </div>
  </div>
</template>

<script setup>
import TextLayerEditor from './TextLayerEditor.vue'
import ImageLayerEditor from './ImageLayerEditor.vue'
import ShapeLayerEditor from './ShapeLayerEditor.vue'
import {  defineProps } from 'vue';
const props = defineProps({
  selectedLayerIndex: Number,
  layer: Object,
});
// const selectedLayer = computed(() => {
//   return props.layoutTemplate ? props.layoutTemplate.value.layers : ''
//         // props.layoutTemplate.layers[props.selectedLayerIndex];
// });

</script>

<style></style>
