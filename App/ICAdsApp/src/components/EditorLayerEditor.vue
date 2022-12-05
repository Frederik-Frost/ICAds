<template>
  <div>
    <h3>Layer {{ props.selectedLayerIndex + 1 }}</h3>
    <div v-if="layer">
      <div class="bg-white p-2 rounded shadow">
        <TextLayerEditor v-if="layer.layerType == 'TextLayer'" :layer="layer" class="flex flex-col gap-2" />
        <ImageLayerEditor v-if="layer.layerType == 'ImageLayer'" :layer="layer" class="flex flex-col gap-2" />
        <ShapeLayerEditor v-if="layer.layerType == 'ShapeLayer'" :layer="layer" class="flex flex-col gap-2" />
      </div>
    </div>
    <div v-else class="bg-white p-2 rounded shadow">
      <p>Select a layer</p>
    </div>
  </div>
</template>

<script setup>
import TextLayerEditor from './TextLayerEditor.vue'
import ImageLayerEditor from './ImageLayerEditor.vue'
import ShapeLayerEditor from './ShapeLayerEditor.vue'
import { ref, onMounted, defineProps, computed } from 'vue';
import fonts from './../assets/js/Fonts.json';
const props = defineProps({
  selectedLayerIndex: Number,
  layer: Object,
});
// const selectedLayer = computed(() => {
//   return props.layoutTemplate ? props.layoutTemplate.value.layers : ''
//         // props.layoutTemplate.layers[props.selectedLayerIndex];
// });

onMounted(() => {
  // console.log(props.selectedLayerIndex)
  // console.log(props.selectedLayerIndex.value)
  //   selectedLayer.value =
});
</script>

<style></style>
