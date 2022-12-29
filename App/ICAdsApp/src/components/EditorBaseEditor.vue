<template>
  <div>
    <header class="flex flex-row gap-4 items-center bg-primary2half p-4 rounded-t-lg shadow-md justify-between">
        <h3 class="text-white">Layout</h3>
        <p class=" text-primary2quarter">{{store.layout.name}}</p>
    </header>

    <div class="bg-white p-2">
        <h4>Dimensions</h4>
        
      <!-- <div class="form-group">
            {{layout}}
        </div> -->
      <div class="form-group">
        <label for="height">Layout width</label>
        <input type="number" name="height" v-model="layout.width" />
      </div>
      <div class="form-group">
        <label for="height">Layout height</label>
        <input type="number" name="height" v-model="layout.height" />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, nextTick } from 'vue';
import { useOrgStore } from './../stores/organization';
const store = useOrgStore();
const props = defineProps({
  layout: Object,
});
</script>

<style></style>
