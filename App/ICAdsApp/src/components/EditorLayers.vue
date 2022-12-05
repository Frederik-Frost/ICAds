<template>
  <div>
    <h3>Layers</h3>

    <div class="pb-2">
      <div
        v-for="(layer, index) in layoutTemplate.layers"
        :key="index"
        class="bg-white p-2 mb-2 rounded shadow cursor-pointer flex flex-row justify-between"
        :class="{ 'selected-layer': selectedLayerIndex == index }"
        @click="$emit('selectLayer', index)"
      >
        <div>
          <span>Layer {{ index + 1 }} - </span>
          <span>{{ layer.layerType }}</span>
        </div>
        <button @click="$emit('removeLayer', index)" class="flex items-center">
          <span class="material-symbols-outlined text-charcoal50 hover:text-flame"> delete </span>
        </button>
      </div>

      <CustomDropdown title="test">
        <template v-slot:toggle>
          <div class="text-left p-2 rounded shadow w-full text-charcoal bg-white hover:bg-alabaster font-bold">+ Layer</div>
        </template>
        <div class="flex flex-col justify-start">
          <button class="text-left p-2 hover:bg-alabaster" @click="$emit('newLayer', 'ImageLayer')">Image Layer</button>
          <button class="text-left p-2 hover:bg-alabaster" @click="$emit('newLayer', 'TextLayer')">Text Layer</button>
          <button class="text-left p-2 hover:bg-alabaster" @click="$emit('newLayer', 'ShapeLayer')">Shape Layer</button>
        </div>
      </CustomDropdown>
      <!-- <button class="btn-tertiary" @click="$emit('newLayer')">+ Layer</button> -->
    </div>
  </div>
</template>
<script setup>
import CustomDropdown from './CustomDropdown.vue';
import { ref, onMounted, defineProps, computed } from 'vue';
const props = defineProps({
  layoutTemplate: Object,
  selectedLayerIndex: Number,
});
</script>

<style></style>
