<template>
  <div class="w-[500px] max-h-[400px] overflow-y-auto">
    <div v-if="!groups">
      <p class="text-xs text-opacity-50 p-2">No variables - please load a product</p>
    </div>
    <div v-else>
      <div class="flex flex-row justify-between items-center my-1 px-4 hover:bg-hoverWhite" v-for="(variable, index) in groups.variables" :key="index">
        <span :class="[groups.root ? '': ' translate-x-4']">
          {{ variable.prettyName || variable.name }}
        </span>
        <a
          @click="copyToClipboard(variable.name), (clicked = true)"
          class="flex flex-row items-center group cursor-pointer"
          :tooltip="clicked ? 'Copied!' : 'Copy value: ' + variable.name"
          tooltip-placement="left"
          @mouseleave="clicked = false"
        >
          <span class="text-opacity-50 text-right whitespace-nowrap overflow-hidden text-ellipsis max-w-[300px]">
            {{ variable.value || 'No value' }}
          </span>
          <span class="material-symbols-outlined scale-75 opacity-0 group-hover:opacity-100"> content_copy </span>
        </a>
      </div>

      <div class="flex flex-col justify-start " v-for="(group, index) in groups.groups" :key="index">
        <a @click.stop="toggleGroup(index)" class="flex flex-row justify-between cursor-pointer hover:bg-hoverWhite px-4" >
          <span class="font-bold" >
            {{ group.name }}
          </span>
          <span class="material-symbols-outlined"> {{ collapsedGroups[index] ? 'expand_more' : 'expand_less' }} </span>
        </a>
        
        <div v-if="collapsedGroups[index]">
          <VariableBrowser :variableGroups="group"/>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed, defineProps, onMounted, ref, useAttrs } from 'vue';
import useClipboard from 'vue-clipboard3';

const props = defineProps({
  variableGroups: Object,
});
const attrs = useAttrs();
const groups = computed(() => {
  return props.variableGroups ? props.variableGroups : attrs.productVariables;
});

const collapsedGroups = ref({});
const toggleGroup = (index) => {
  console.log('hello');
  collapsedGroups.value[index] = !collapsedGroups.value[index];
};
const isCollapsed = (index) => {
  return collapsedGroups.value[index] == true;
};

const clicked = ref(false);
const { toClipboard } = useClipboard();
const copyToClipboard = async (value) => {
  console.log('first');
  try {
    await toClipboard(value);
    console.log('Copied to clipboard');
  } catch (e) {
    console.error(e);
  }
};
</script>

<style></style>
