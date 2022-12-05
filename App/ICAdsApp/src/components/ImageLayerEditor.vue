<template>
  <div class="flex flex-col gap-2">
    <div class="flex flex-col gap-2 border-b border-charcoal50 pb-2 mb-2">
      <div class="form-group">
        <label for="source">Source (Variable)</label>
        <input type="text" name="source" v-model="layer.source" />
      </div>
      <div class="form-group-y">
        <label for="width">width</label>
        <input type="number" name="width" v-model="layer.imageWidth" />
      </div>
      <div class="form-group-y">
        <label for="height">height</label>
        <input type="number" name="height" v-model="layer.imageHeight" />
      </div>
      <div class="form-group">
        <label for="fit">Image fit</label>
        <input type="text" name="fit" v-model="layer.imageHeight" />
      </div>
      <div class="flex gap-4">
        <!-- <label class="text-charcoal">Background style</label> -->
        <div class="form-group">
          <label class="text-charcoal" for="fit">Fit</label>
          <input class="self-start mt-1" type="radio" id="fit" name="imageFit" value="Fit" v-model="layer.objectFit" />
        </div>
        <div class="form-group">
          <label class="text-charcoal" for="cover">Cover</label>
          <input
            class="self-start mt-1"
            type="radio"
            id="cover"
            name="imageFit"
            value="Cover"
            v-model="layer.objectFit"
          />
        </div>
      </div>
      <div class="form-group">
        <label for="alignHorizontal">Horizontal alignment</label>
        <select
          name="alignHorizontal"
          v-model="layer.alignHorizontal"
          class="p-2 border rounded-md border-charcoal50 hover:border-charcoal focus:border-charcoal focus:outline-none text-sm"
        >
          <option value="Left">Left</option>
          <option value="Center">Center</option>
          <option value="Right">Right</option>
        </select>
      </div>

      <div class="form-group">
        <label for="alignVertical">Vertical alignment</label>
        <select
          name="alignVertical"
          v-model="layer.alignVertical"
          class="p-2 border rounded-md border-charcoal50 hover:border-charcoal focus:border-charcoal focus:outline-none text-sm"
        >
          <option value="Top">Top</option>
          <option value="Center">Center</option>
          <option value="Bottom">Bottom</option>
        </select>
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
