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
          ref="previewimg"
          :style="{ width:  canvasMeasurements?.width + 'px', height: canvasMeasurements?.height + 'px' }"
          class="absolute z-0"
        ></div>

        <div v-for="(layer, index) in layoutTemplate.layers" :key="index" class="">
          <div v-if="layer.layerType == 'TextLayer'" class="absolute z-0" :style="getTextLayerStyles(layer)">
            {{ layer.text }}
          </div>
          <div v-if="layer.layerType == 'ImageLayer'" class="absolute z-0" :style="getImageLayerStyles(layer)"></div>
          <div v-if="layer.layerType == 'ShapeLayer'" class="absolute z-0" :style="getShapeLayerStyles(layer)"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import EditorHelper from './../assets/js/EditorHelper';
import { ref, onMounted, defineProps, computed, onBeforeUnmount, nextTick, watch } from 'vue';
const canvas = ref();
const ready = ref(false);
const props = defineProps({
  layoutTemplate: Object,
  selectedProduct: Object,
  base64ImgString: String
});

const test = ref(false);
const canvasMeasurements = computed(() => {
  return canvas.value ? canvas.value.getBoundingClientRect() : null;
});
// factor = desired width / actual width or desired height / actual height
const factor = computed(() => {
  return canvasMeasurements.value ? canvasMeasurements.value.width / props.layoutTemplate.width : 1;
});

watch(
  () => props.base64ImgString,
  () => handleAddPreviewImg(props.base64ImgString)
);

const previewimg = ref();
const handleAddPreviewImg = (baseString) => {
  EditorHelper.Base64ToImage(baseString, function (img) {
    previewimg.value.innerHTML = '';
    previewimg.value.appendChild(img);
  });
};

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
    borderStyle: 'dashed',
  };
  return styles;
};

const getShapeLayerStyles = (layer) => {
  let styles = {
    top: `${layer.posY * factor.value}px`,
    left: `${layer.posX * factor.value}px`,
    width: `${layer.width * factor.value}px`,
    height: `${layer.height * factor.value}px`,
    background: layer.backgroundColor,
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
