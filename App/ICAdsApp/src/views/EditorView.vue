<template>
  <div>
    <EditorSubHeader />

    <!-- Image area here  -->
    <button @click="testGeneration()">Click to test</button>
    <!-- Editing area here  -->
    <!-- Layers area here  -->
    <div v-if="success">
      <img :src="generatedImg" alt="" />
    </div>
  </div>
</template>

<script setup>
import EditorSubHeader from './../components/EditorSubHeader.vue';
import { ref, onMounted } from 'vue';
import { useOrgStore } from './../stores/organization';
import Template from './../assets/js/Template';
import testTemp from './../assets/js/testTemp.json';
const searchTerm = ref('');
const store = useOrgStore();
store.selectedProduct;
store.layout;

onMounted(() => {
  const template = new Template(testTemp);
  console.log(template);
});

const success = ref(false);
const generatedImg = ref('');
const testGeneration = () => {
  const template = new Template(testTemp);
  console.log(template);
  console.log(template.export());
  store.testfind(template.export()).then(res => {
    console.log(res)
    generatedImg.value = res
    success.value = true
  })
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

// const searchProducts = (e) => {
//   store.searchProduct(searchTerm.value)
//   console.log(e)
//   console.log(searchTerm.value)
// }
</script>

<style></style>
