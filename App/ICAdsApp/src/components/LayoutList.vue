<template>
  <div id="layoutList" class="">
    <header class="w-full grid grid-cols-6 rounded border py-2 bg-white border-charcoal50 px-4 items-center h-[42px]">
      <div class="flex items-center text-charcoal50">
        <a href="#">Layout Title</a>
        <span class="material-symbols-outlined text-charcoal50"> expand_more </span>
      </div>
      <div>
        <a href="#" class="text-charcoal50">Type</a>
        <span class="material-symbols-outlined text-charcoal50"> expand_more </span>
      </div>
      <div>
        <a href="#" class="text-charcoal50">Created</a>
        <span class="material-symbols-outlined text-charcoal50"> expand_more </span>
      </div>
      <div>
        <a href="#" class="text-charcoal50">Updated</a>
        <span class="material-symbols-outlined text-charcoal50"> expand_more </span>
      </div>
      <div>
        <a href="#" class="text-charcoal50">Data</a>
        <span class="material-symbols-outlined text-charcoal50"> expand_more </span>
      </div>
    </header>

    <main class="w-full mt-4">
      <div
        v-for="(layout, index) in orgStore.layouts"
        :key="layout.id"
        class="grid grid-cols-6 bg-white rounded shadow py-2 mt-2"
      >
        <div class="flex items-center pl-4">
          <span class="material-symbols-outlined"> chevron_right </span>
          <button
            class="link underline pl-4"
            @click="$router.push({ name: 'editor', params: { layoutId: layout.id } })"
          >
            {{ layout.name }}
          </button>
        </div>
        <div class="flex items-center">
          <span>{{ layout.isPublic ? 'Community' : 'Private' }}</span>
        </div>
        <div class="flex items-center">
          <span>{{ formatDate(layout.created) }}</span>
        </div>
        <div class="flex items-center">
          <span>{{ formatDate(layout.updated) }}</span>
        </div>

        <IntegrationSelect
          :modelValue="layout.integrationId"
          @updateValue="
            (id) => {
              updateLayoutIntegration(layout, id);
            }
          "
          :name="`integrationselect-${index}`"
          :id="`integrationselect-${index}`"
        />

        <div class="flex items-center justify-end pr-4 gap-4">
          <button class="btn-secondary">Export</button>
          <button @click="deleteLayout(layout.id)" class="flex items-center">
            <span class="material-symbols-outlined text-charcoal50 hover:text-flame"> delete </span>
          </button>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { computed, reactive, defineComponent, onMounted, ref, defineProps } from 'vue';
import { useOrgStore } from '../stores/organization';
import IntegrationSelect from './IntegrationSelect.vue';
const orgStore = useOrgStore();
const props = defineProps({
  layouts: Array,
});
const formatDate = (dateString) => {
  const date = new Date(dateString);
  let options = {
    year: 'numeric',
    month: 'numeric',
    day: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
  };
  return new Intl.DateTimeFormat('default', options).format(date);
};

const updateLayoutIntegration = (layout, id) => {
  layout.integrationId = id;
  orgStore.updateLayout(layout).then((res) => {
    console.log(res);
  });
};

const deleteLayout = (id) => {
  orgStore.deleteLayout(id).then((res) => {
    console.log(res);
  });
};
</script>

<style></style>
