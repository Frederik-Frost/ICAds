<template>
  <div class="flex flex-col gap-2">
    <div class="flex flex-col gap-2 border-b border-charcoal50 pb-2 mb-2">
      <div class="form-group-y">
        <label for="width">width</label>
        <input type="number" name="width" v-model="layer.width" />
      </div>
      <div class="form-group-y">
        <label for="height">height</label>
        <input type="number" name="height" v-model="layer.height" />
      </div>
      <div class="form-group-y">
        <label for="posX">Position X</label>
        <input type="number" name="posX" v-model="layer.posX" />
      </div>
      <div class="form-group-y">
        <label for="posY">Position Y</label>
        <input type="number" name="posY" v-model="layer.posY" />
      </div>
    </div>
    <div class="form-group-y">
      <label for="borderRadius">Border radius</label>
      <input type="number" name="borderRadius" v-model="layer.borderRadius" />
    </div>
    <div class="form-group">
      <label class="cursor-pointer" for="textColor">Background color </label>
      <div class="flex flex-row items-center justify-between gap-2">
        <input class="cursor-pointer" type="color" name="textColor" v-model="layer.backgroundColor" />
        <input class="flex-1" type="text" name="textColor" v-model="layer.backgroundColor" />
      </div>
    </div>

    <div class="flex gap-4">
      <!-- <label class="text-charcoal">Background style</label> -->
      <div class="form-group">
        <label class="text-charcoal" for="fill">Fill</label>
        <input
          class="self-start mt-1"
          type="radio"
          id="fill"
          name="backgroundStyle"
          value="Fill"
          v-model="layer.backgroundStyle"
        />
      </div>
      <div class="form-group">
        <label class="text-charcoal" for="stroke">Stroke</label>
        <input
          class="self-start mt-1"
          type="radio"
          id="stroke"
          name="backgroundStyle"
          value="Stroke"
          v-model="layer.backgroundStyle"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, defineProps, computed } from 'vue';
const props = defineProps({
  selectedLayerIndex: Number,
  layer: Object,
});
</script>

<style></style>
