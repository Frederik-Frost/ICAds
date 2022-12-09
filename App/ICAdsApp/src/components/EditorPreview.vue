<template>
  <div class="">
    <h3>preview</h3>
    <div class="relative max-h-[700px] flex">
      <div
        id="templateCanvas"
        v-if="ready"
        ref="canvas"
        :style="{ aspectRatio: layoutTemplate.width / layoutTemplate.height, width: layoutTemplate.width + 'px' }"
        class="border border-charcoal border-dashed overflow-hidden relative"
      >
        <div
          v-if="selectedProduct"
          :style="{ width: canvasMeasurements ?? width + 'px', height: canvasMeasurements ?? height + 'px' }"
        >
          <!-- <img
            :src="selectedProduct.images[0].src"
            alt="Preview image"
            :style="{ width: (canvasMeasurements ?? width) + 'px' }"
            class="object-cover"
          /> -->
        </div>
        <div v-for="(layer, index) in layoutTemplate.layers" :key="index" >
         
          <div v-if="layer.layerType == 'TextLayer'" class="absolute" :style="getTextLayerStyles(layer)">
            {{ layer.text }}
          </div>
          <div v-if="layer.layerType == 'ImageLayer'" class="absolute" :style="getImageLayerStyles(layer)"></div>
          <div v-if="layer.layerType == 'ShapeLayer'" class="absolute" :style="getShapeLayerStyles(layer)"></div>

          <!-- {{ layer.text }} -->
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, defineProps, computed, onBeforeUnmount, nextTick } from 'vue';
const canvas = ref();
const ready = ref(false);
const props = defineProps({
  layoutTemplate: Object,
  selectedProduct: Object,
});

const canvasMeasurements = computed(() => {
  return canvas.value ? canvas.value.getBoundingClientRect() : null;
});
// factor = desired width / actual width or desired height / actual height
const factor = computed(() => {
  return canvasMeasurements.value ? canvasMeasurements.value.width / props.layoutTemplate.width : 1;
});

const getTextLayerStyles = (layer) => {
  let styles = {
    top: `${layer.posY * factor.value}px`,
    left: `${layer.posX * factor.value}px`,
    color: layer.textColor,
    fontFamily: `"${layer.fontFamily}"`,
    fontSize: `${layer.textSize * factor.value}px`,
  };

  if (layer.hasBackground) {
    let backgroundStyles = {
      padding: `${layer.inflateY * factor.value}px ${layer.inflateX * factor.value}px`,
      borderRadius: `${layer.borderRadius * factor.value}px`,
    };
    if (layer.backgroundStyle === 'Fill') {
      backgroundStyles.backgroundColor = layer.backgroundColor;
    }

    styles = { ...styles, ...backgroundStyles };
    console.log(styles);
  }
  return styles;
};

const getImageLayerStyles = (layer) => {
  let styles = {
    top: `${layer.posY * factor.value}px`,
    left: `${layer.posX * factor.value}px`,
    width: `${layer.width * factor.value}px`,
    height: `${layer.height * factor.value}px`,
    objectFit: `${layer.objectFit}`,
    // Alignment not created yet
    borderColor: '#000',
    borderWidth: '1px', 
    borderStyle: 'dashed'
  };
  return styles;
};
const getShapeLayerStyles = (layer) => {
  let styles = {
    top: `${layer.posY * factor.value}px`,
    left: `${layer.posX * factor.value}px`,
    width: `${layer.width * factor.value}px`,
    height: `${layer.height * factor.value}px`,
    background:  layer.backgroundColor,
    border: layer.backgroundStyle == 'Stroke' ? layer.backgroundColor : 'none',
    borderRadius: layer.borderRadius,
    
    
  };
  return styles;
};

const rearrangeLayout = () => {
  ready.value = false;
  nextTick(() => {
    ready.value = true;
  });
};

onMounted(() => {
  ready.value = true;
  window.addEventListener('resize', rearrangeLayout);
});
onBeforeUnmount(() => {
  window.removeEventListener('resize', rearrangeLayout);
});
</script>

<style>
#templateCanvas {
 
  max-width: clamp(400px, 100%, 700px);
}
</style>
