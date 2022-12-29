<template>
  <div class="mt-4 p-2">
    <h3 class="pl-2">Layers</h3>
    <div class="pb-2">
      <div
        v-for="(layer, index) in layoutTemplate.layers"
        :key="index"
        class=" p-2 mb-2 rounded shadow cursor-pointer flex flex-row justify-between hover:bg-primary2quarter"
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

      <CustomDropdown title="addLayer">
        <template v-slot:toggle>
          <div class="text-left p-2 rounded shadow w-full text-charcoal bg-white hover:bg-primary2quarter font-bold">
            + Layer
          </div>
        </template>

        <template v-slot:content>
          <div class="flex flex-col justify-start">
            <button
              v-for="(type, index) in ['ImageLayer', 'TextLayer', 'ShapeLayer']"
              :key="index"
              @click="$emit('newLayer', type)"
              class="text-left p-2 hover:bg-primary2quarter"
            >
              {{ getPrettyName(type) }}
            </button>
          </div>
        </template>
      </CustomDropdown>
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

const getPrettyName = (type) => {
  switch (type) {
    case 'ImageLayer':
      return 'Image Layer';
    case 'TextLayer':
      return 'Text Layer';
    case 'ShapeLayer':
      return 'Shape Layer';
  }
};
</script>

<style></style>
