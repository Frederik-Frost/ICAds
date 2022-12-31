<template>
  <div class="max-w-screen-xl mx-auto mt-4 px-4">
    <h1 class="text-4xl mb-4">Export Your images here</h1>
    <div class="grid grid-cols-8 gap-12">
      <div class="col-span-3">
        <div class="flex flex-col">
          <label>Select Layout</label>
          <select
            name="layoutSelect"
            id="layoutSelect"
            v-model="selectedLayout"
            class="p-2 border border-charcoal50 rounded-md hover:shadow focus:shadow cursor-pointer"
            :disabled="loadingTemplate"
          >
            <option value="">none</option>
            <option v-for="layout in orgStore.layouts" :key="layout.id" :value="layout.id">
              {{ layout.name }}
            </option>
          </select>
        </div>

        <div
          :class="!loadingTemplate && templateIsPresent ? [] : ['opacity-50', 'pointer-events-none']"
          class="px-2 my-2 bg-white rounded-md border border-charcoal50 hover:shadow focus-within:shadow"
        >
          <SearchDropdown @update="searchProduct" @select="getProductVariables" :results="products" />
        </div>

        <div ref="layoutlist" class="bg-white flex flex-col flex-auto gap-2 rounded-md shadow mb-4 overflow-auto" :style="{ height: `calc(100vh - ${previewBounds.top + 200}px)` }">
          <h3 class="p-2">Selected for export</h3>
          <div v-if="exportList.length == 0">
            <p class="text-charcoal50 text-xs p-2">No products selected for export...</p>
          </div>
          <div v-for="(exportable, index) in exportList" :key="index" class="hover:bg-hoverWhite cursor-pointer flex items-center justify-between p-2">
            <span> {{ exportable.title }} </span>
            <a @click="removeFromExportList(index)" class="flex items-center opacity-50 hover:opacity-100"><span class="material-symbols-outlined"> close </span></a>
          </div>
        </div>

        <div class="flex gap-4">
          <button class="btn-tertiary" @click="generatePreviews">Preview</button>
          <button class="btn-primary" @click="exportImages" :disabled="exportList.length == 0">Export</button>
        </div>
      </div>

      <div
        ref="preview"
        class="col-span-5 overflow-auto pb-6"
        :style="{ maxHeight: `calc(100vh - ${previewBounds.top}px)` }"
      >
        <h3>Image previews</h3>
        <div class="grid grid-cols-2 gap-4">
          <div v-if="exportList.length == 0">
            <p>No products selected for export...</p>
          </div>
          <div
            v-for="(preview, index) in exportList"
            :key="preview.id"
            class="col-span-1 p-2 bg-primary2half rounded shadow"
          >
            <div :id="`previewimg${index}`" class="relative h-full">
              <div class="flex flex-col items-center justify-center h-full">
                <span class="material-symbols-outlined"> image </span>
                <p>{{ preview.title }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import IntegrationSelect from './../components/IntegrationSelect.vue';
import Template from './../assets/js/Template';
import EditorHelper from './../assets/js/EditorHelper';
import SearchDropdown from './../components/SearchDropdown.vue';
import { ref, onMounted, nextTick, computed, watch } from 'vue';
import { useOrgStore } from '../stores/organization';
const orgStore = useOrgStore();

const selectedLayout = ref('');
const integrationReady = ref(false);
const loadingTemplate = ref(false);
const templateIsPresent = ref(false);
watch(
  () => selectedLayout.value,
  () => {
    console.log(selectedLayout.value);
    templateIsPresent.value = false;
    if (selectedLayout.value) loadTemplate(selectedLayout.value);
  }
);
const loadTemplate = (layoutId) => {
  loadingTemplate.value = true;
  orgStore.getLayout(layoutId).then((res) => {
    console.log(res);
    orgStore.$patch((state) => (state.layoutTemplate = new Template(res.data.template.templateJSON)));
    loadingTemplate.value = false;
    templateIsPresent.value = true;
  });
};

const products = ref([]);
const searchProduct = (query) => {
  console.log(query);
  orgStore.searchProduct(query, selectedLayout.value).then((res) => {
    console.log(res);
    products.value = res.data.products.edges;
  });
};

const getProductVariables = (product) => {
  console.log(product);
  orgStore.getProductVariables(product.id, selectedLayout.value).then((res) => {
    console.log(res);
    addToExportList(product, res);
  });
};

const exportList = ref([]);
const previewRefs = ref([]);
const addToExportList = (product, variables) => {
  const exportObject = {
    ...product,
    variables: variables,
  };
  previewRefs.value.push({ [`previewimg${exportList.value.length}`]: ref() });
  console.log(previewRefs.value);
  exportList.value.push(exportObject);
};

const generatePreviews = () => {
  const exportedTemplate = orgStore.layoutTemplate.export();
  exportList.value.forEach((e, index) => {
    orgStore.generateImagePreview(exportedTemplate, e.variables).then((res) => {
      nextTick(() => handleAddPreviewImg(`data:image/png;base64,${res}`, index));
    });
  });
};
const handleAddPreviewImg = (baseString, index) => {
  EditorHelper.base64ToImage(baseString, function (img) {
    const preview = document.querySelector(`#previewimg${index}`);
    console.log(preview);
    preview.innerHTML = '';
    preview.appendChild(img);
  });
};

const preview = ref();
const previewBounds = computed(() => {
  return preview.value ? preview.value.getBoundingClientRect() : { top: 0 };
});

const layoutlist = ref()
const layoutlistBounds = computed(() => {
  return layoutlist.value ? layoutlist.value.getBoundingClientRect() : { top: 0 };
});

const removeFromExportList = (index) => {
  exportList.value.splice(index, 1)
}

const exportImages = () => {
  console.log('first');
  const exportedTemplate = orgStore.layoutTemplate.export();
  let newList = []
  exportList.value.forEach(e => {
    newList.push({ template: exportedTemplate, variables:  e.variables})
  })

  orgStore.exportImageZip(newList)
  
};
</script>

<style></style>
