<template>
  <div>
    <EditorSubHeader />
    <main class="grid grid-cols-8 gap-2 max-w-screen-2xl mx-auto mt-8 p-4" v-if="store.layoutTemplate">
      <!-- Image area here  -->
      <EditorPreview
        :layoutTemplate="store.layoutTemplate"
        :selectedProduct="store.selectedProduct"
        class="col-span-4"
      />
      <!-- Editing area here  -->
      <EditorLayerEditor
        :layer="selectedLayerIndexIsSet ? store.layoutTemplate.layers[selectedLayerIndex] : null"
        :selectedLayerIndex="selectedLayerIndex"
        class="col-span-2"
      />

      <!-- Layers area here  -->
      <div class="col-span-2 flex flex-col">
        <EditorBaseEditor :layout="store.layoutTemplate" />
        <EditorLayers
          :layoutTemplate="store.layoutTemplate"
          :selectedLayerIndex="selectedLayerIndex"
          @selectLayer="
            (index) => {
              selectedLayerIndex = index;
            }
          "
          @newLayer="(layerType) => addLayer(layerType)"
          @removeLayer="
            (index) => {
              removeLayer(index);
            }
          "
        />
      </div>
    </main>
  </div>
</template>

<script setup>
import EditorSubHeader from './../components/EditorSubHeader.vue';
import EditorPreview from './../components/EditorPreview.vue';
import EditorLayerEditor from './../components/EditorLayerEditor.vue';
import EditorLayers from './../components/EditorLayers.vue';
import EditorBaseEditor from './../components/EditorBaseEditor.vue';
import { ref, onMounted, nextTick, computed } from 'vue';
import { useOrgStore } from './../stores/organization';
import Template from './../assets/js/Template';
import TextLayer from './../assets/js/TextLayer';
import testTemp from './../assets/js/testTemp.json';
const searchTerm = ref('');
const main = ref();
const store = useOrgStore();
const selectedLayerIndex = ref(null);
const selectedLayerIndexIsSet = computed(() => {
  return selectedLayerIndex.value != null;
});

const removeLayer = (index) => {
  selectedLayerIndex.value = 0;
  store.$patch((state) => {
    state.layoutTemplate.layers.splice(index, 1);
  });
};

const addLayer = (layerType) => {
  store.$patch((state) => {
    state.layoutTemplate.layers.push(state.layoutTemplate.createLayertype({ layerType: layerType }));
  });
};
</script>

<style></style>
