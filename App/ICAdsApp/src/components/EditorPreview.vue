<template>
  <div class="">
    <div class="flex flex-row gap-4">
      <h3>preview</h3>
      <div>
        <button type="button" @click="showGeneratedPreivew = false" :class="{ 'text-primary2': !showGeneratedPreivew }">
          Layout
        </button>
        <button
          type="button"
          @click="(showGeneratedPreivew = true), generatePreview()"
          :class="{ 'text-primary2': showGeneratedPreivew }"
        >
          Generate preview
        </button>
      </div>
    </div>
    <div class="relative max-h-[700px] flex">
      <div
        id="templateCanvas"
        v-if="ready"
        ref="canvas"
        :style="{ aspectRatio: layoutTemplate.width / layoutTemplate.height, width: layoutTemplate.width + 'px' }"
        class="border border-charcoal border-dashed overflow-hidden relative"
      >
        <div
          v-if="showGeneratedPreivew"
          ref="previewimg"
          :style="{ width: canvasMeasurements?.width + 'px', height: canvasMeasurements?.height + 'px' }"
          class="absolute z-0"
        >
          <span
            v-if="generating"
            class="material-symbols-outlined text-5xl flex flex-col justify-center items-center h-full"
          >
            pending
          </span>
          <div v-else-if="generateError" class="flex flex-col justify-center items-center h-full">
            <span class="material-symbols-outlined text-flame text-5xl"> error </span>
            <p class="text-center w-[300px]">
              Could not load the preview image - Some variables might be missing in the available data
            </p>
          </div>
        </div>
        <div v-else>
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
  </div>
</template>

<script setup>
import EditorHelper from './../assets/js/EditorHelper';
import { useOrgStore } from './../stores/organization';
import { ref, onMounted, defineProps, computed, onBeforeUnmount, nextTick, watch } from 'vue';
const canvas = ref();
const ready = ref(false);
const props = defineProps({
  layoutTemplate: Object,
  selectedProduct: Object,
  base64ImgString: String,
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

const showGeneratedPreivew = ref(false);
// watch(
//   () => showGeneratedPreivew.value,
//   (val, oldVal) => {
//     if (val === true) generatePreview();
//   }
// );

const store = useOrgStore();
const generating = ref(false);
const generateError = ref(false);
const base64StringLoaded = ref(false);
const generatePreview = () => {
  generating.value = true;
  generateError.value = false;
  store
    .testGenerateTemp(store.layoutTemplate.export())
    .then((res) => {
      nextTick(() => handleAddPreviewImg(`data:image/png;base64,${res}`));
      generating.value = false;
    })
    .catch((e) => {
      generating.value = false;
      generateError.value = true;
    });
};
const previewimg = ref();
const handleAddPreviewImg = (baseString) => {
  EditorHelper.Base64ToImage(baseString, function (img) {
    previewimg.value.innerHTML = '';
    previewimg.value.appendChild(img);
  });
};

// watch(
//   () => props.base64ImgString,
//   () => handleAddPreviewImg(props.base64ImgString)
// );
</script>
<style>
#templateCanvas {
  max-width: clamp(400px, 100%, 700px);
}
</style>
