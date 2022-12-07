<template>
<div>
    <h3>Layout dimensions</h3>
    
    <div class="bg-white p-2 rounded shadow">
        <!-- <div class="form-group">
            {{layout}}
        </div> -->
        <div class="form-group">
            <label for="height">Layout height</label>
            <input type="number" name="height" v-model="layout.height">
        </div>
        <div class="form-group">
            <label for="height">Layout width</label>
            <input type="number" name="height" v-model="layout.width">
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

<style>

</style>