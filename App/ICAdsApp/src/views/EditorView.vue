<template>
  <div>
    <EditorSubHeader />

    <main class="grid grid-cols-8 gap-2 p-2" v-if="store.layoutTemplate">
      <!-- Image area here  -->
      <EditorPreview
        :layoutTemplate="store.layoutTemplate"
        :selectedProduct="store.selectedProduct"
        class="col-span-4"
      />
      <!-- Editing area here  -->
      <EditorLayerEditor
        :layer="store.layoutTemplate.layers[selectedLayerIndex]"
        :selectedLayerIndex="selectedLayerIndex"
        class="col-span-2"
      />
      <!-- Layers area here  -->
      <EditorLayers
        :layoutTemplate="store.layoutTemplate"
        :selectedLayerIndex="selectedLayerIndex"
        class="col-span-2"
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
    </main>

    <button @click="testVariables()">Click to test</button>
    <!-- <button @click="testGeneration()">Click to test</button> -->
    <div v-if="success">
      <img :src="generatedImg" alt="" />
    </div>
  </div>
</template>

<script setup>
import EditorSubHeader from './../components/EditorSubHeader.vue';
import EditorPreview from './../components/EditorPreview.vue';
import EditorLayerEditor from './../components/EditorLayerEditor.vue';
import EditorLayers from './../components/EditorLayers.vue';
import { ref, onMounted, nextTick } from 'vue';
import { useOrgStore } from './../stores/organization';
import Template from './../assets/js/Template';
import TextLayer from './../assets/js/TextLayer';
import testTemp from './../assets/js/testTemp.json';
const searchTerm = ref('');
const selectedLayerIndex = ref(0);
const store = useOrgStore();

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

// onMounted(() => {
//   const template = new Template(testTemp);
//   console.log(template);
//   store.$patch((state) => {
//     state.layoutTemplate = template;
//   });
// });

const success = ref(false);
const generatedImg = ref('');
const testGeneration = () => {
  const template = new Template(testTemp);
  console.log(template);
  console.log(template.export());
  store.testfind(template.export()).then((res) => {
    console.log(res);
    generatedImg.value = res;
    success.value = true;
  });
  // store.testGenerateTemp(template.export()).then((res) => {
  //   console.log(res);
  // let base64String = btoa( new Uint8Array(res));
  // generatedImg.value = 'data:image/png;base64,' + base64String;
  // console.log( base64String)
  // success.value = true

  // const imageBytes = res.arrayBuffer();
  // var blob = new Blob([res], { type: "image/png" });
  // var imageUrl = URL.createObjectURL(blob);
  // generatedImg.value = imageUrl;
  // success.value = true
  // });
};


const testVariables = () => {
  store.testVars().then(res => console.log(res))
}

// const searchProducts = (e) => {
//   store.searchProduct(searchTerm.value)
//   console.log(e)
//   console.log(searchTerm.value)
// }
</script>

<style></style>
