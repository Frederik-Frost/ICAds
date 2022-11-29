<template>
  <div>
    <EditorSubHeader />


    <!-- Image area here  -->
    <!-- Editing area here  -->
    <!-- Layers area here  -->





    
  </div>
</template>

<script setup>
import EditorSubHeader from './../components/EditorSubHeader.vue';
import { ref, onMounted } from 'vue';
import { useOrgStore } from './../stores/organization';
const searchTerm = ref('');
const store = useOrgStore();
store.selectedProduct;
store.layout;

// const searchProducts = (e) => {
//   store.searchProduct(searchTerm.value)
//   console.log(e)
//   console.log(searchTerm.value)
// }
</script>

<style></style>
