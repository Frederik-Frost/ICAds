<template>
  <div class="form-group">
    <label :for="name">{{ label }}</label>
    <input :type="type" :name="name" :id="id" :value="modelValue" @input="updateValue" :disabled="disabled"/>
  </div>
</template>

<script setup>
import { ref, onMounted, defineProps, computed, defineEmits } from 'vue';
const props = defineProps({
  name: String,
  type: String,
  id: String,
  label: String,
  modelValue: { type: [Number, String] },
  disabled: Boolean
});
const emit = defineEmits();
const updateValue = (event) => {
    emit('update:modelValue', event.target.value)
}
</script>

<style></style>
