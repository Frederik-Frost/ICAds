<template>
  <div id="layoutList" class="">
    <header class="hidden md:grid w-full grid-cols-5 rounded border py-2 bg-white border-charcoal50 px-4 items-center h-[42px]">
      <div class="flex items-center text-charcoal50">
        <a href="#">Layout Title</a>
        <span class="material-symbols-outlined text-charcoal50"> expand_more </span>
      </div>
      <!-- <div>
        <a href="#" class="text-charcoal50">Type</a>
        <span class="material-symbols-outlined text-charcoal50"> expand_more </span>
      </div> -->
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
      <div v-if="orgStore.layouts.length == 0">
        <p v-if="orgStore.integrations.length == 0" class=" text-lg">
          Create your first layout - but first
          <a class="text-primary2 cursor-pointer" @click="$emit('onAddData')">add a data stream!</a>
        </p>
        <p v-else class="font-bold text-lg">
          Now lets create a
          <a class="text-primary2 cursor-pointer" @click="$emit('onCreateLayout')">Layout</a>
        </p>
      </div>
      <div
        v-for="(layout, index) in orgStore.layouts"
        :key="layout.id"
        class="md:grid grid-cols-5 bg-white rounded shadow py-2 mt-2 gap-2"
      >
        <div class="flex items-center pl-4">
          <span class="material-symbols-outlined"> chevron_right </span>
          <button
            class="link underline text-primary2"
            @click="$router.push({ name: 'editor', params: { layoutId: layout.id } })"
          >
            {{ layout.name }}
          </button>
        </div>
        <!-- <div class="flex items-center">
          <span>{{ layout.isPublic ? 'Community' : 'Private' }}</span>
        </div> -->
        <div class="flex items-center ml-4 md:ml-0 my-2 md:my-0">
          <p class="text-charcoal75 w-16 md:hidden">Created</p>
          <span class="font-bold md:font-normal">{{ formatDate(layout.created) }}</span>
        </div>
        <div class="flex items-center ml-4 md:ml-0">
          <p class="text-charcoal75 w-16 md:hidden ">Updated</p>
          <span class="font-bold md:font-normal">{{ formatDate(layout.updated) }}</span>
        </div>
        <div class="flex ml-4 md:ml-0 my-2 md:my-0">
          <p class="text-charcoal75 w-16 md:hidden ">Data</p>
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
        </div>

        <div class="flex items-center justify-end pr-4 gap-4">
          <button class="btn-secondary hidden lg:block">Export</button>
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
  if(dateString == "0001-01-01T00:00:00") return "Never"
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
