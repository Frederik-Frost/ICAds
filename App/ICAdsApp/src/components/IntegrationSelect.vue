<template>
  <select :name="name" :id="id" v-model="selectedId" @change="$emit('updateValue', selectedId);" class="py-2 border border-charcoal50 rounded-md">
    <option value="">none</option>
    <option v-for="integration in orgStore.integrations" :key="integration.id" :value="integration.id">
      {{ integration.name }}
    </option>
  </select>
</template>

<script setup>
import { computed, reactive, defineComponent, onMounted, ref, defineProps } from 'vue';
import { useOrgStore } from '../stores/organization';
const orgStore = useOrgStore();
const props = defineProps({
  id: String,
  name: String,
  modelValue: String
});
const selectedId = ref('');
onMounted(() => {
    selectedId.value =  props.modelValue ? props.modelValue : ""
})
// emits: ['update:modelValue']
</script>

<style></style>
