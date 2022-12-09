<template>
  <div>
    <EditorSubHeader />
    <!-- bg-hoverWhite shadow rounded-md p-4  -->
    <main class="grid grid-cols-8 gap-2 max-w-screen-2xl mx-auto mt-8 p-4" v-if="store.layoutTemplate">
      <!-- Image area here  -->
      <EditorPreview
        :layoutTemplate="store.layoutTemplate"
        :selectedProduct="store.selectedProduct"
        class="col-span-4"
      />

      <!-- <div class="col-span-2 flex flex-col"> -->
      <!-- <EditorBaseEditor :layout="store.layoutTemplate" /> -->
      <!-- Editing area here  -->
      <EditorLayerEditor
        :layer="selectedLayerIndexIsSet ? store.layoutTemplate.layers[selectedLayerIndex] : null"
        :selectedLayerIndex="selectedLayerIndex"
        class="col-span-2"
      />
      <!-- </div> -->
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

    <button @click="testGenerate()">Click to test</button>
    <!-- <button @click="testGeneration()">Click to test</button> -->
    <div ref="main"></div>
    <!-- <div v-if="success" ref="main">
      <img :src="generatedImg" alt="" />
    </div> -->
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
const testGenerate = () => {
  store.testGenerateTemp(store.layoutTemplate.export()).then((res) => {
    const baseStr = `data:image/png;base64,${res}`;



    // downloadFromBase64(baseStr, store.selectedProduct?.id || "image") 

    Base64ToImage(baseStr, function (img) {
      main.value.innerHTML = "";
      main.value.appendChild(img);
      // var log = 'w=' + img.width + ' h=' + img.height;
      // document.getElementById('log').value = log;
    });
  });
};

const Base64ToImage = (base64img, callback) => {
  var img = new Image();
  img.onload = function () {
    callback(img);
  };
  img.src = base64img;
};


const downloadFromBase64 = (base64, fileName) => {
    let link = document.createElement('a');
    link.href = base64;
    link.download = `${fileName}.png`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

const testVariables = () => {
  store.testVars().then((res) => console.log(res));
};

</script>

<style></style>
