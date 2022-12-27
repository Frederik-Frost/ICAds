<template>
  <transition enter-active-class="duration-300 ease-out" enter-from-class="translate-y-[50px]">
    <div class="bg-white h-12 px-4 flex items-center justify-between shadow-sm">
      <div class="flex gap-4">
        <p class="text-lg text-charcoal75">{{ orgStore.layout.name }} -</p>

        <button class="btn-primary text-xs" @click="saveChanges">Save layout</button>
        <button class="btn-secondary text-xs" @click="browseVariables">Browse variables</button>
        <!-- <CustomDropdown title="browseVariables">
          <template v-slot:toggle>
            <button class="btn-secondary text-xs" @click="browseVariables">Browse variables</button>
          </template>
          <template v-slot:content>
            <VariableBrowser :variableGroups="productVariables"/>
          </template>
        </CustomDropdown> -->
      </div>

      <p>
        {{ orgStore.selectedProduct ? orgStore.selectedProduct.title : 'No product data loaded' }}
      </p>
    </div>
  </transition>
</template>

<script setup>
import { $vfm } from 'vue-final-modal';
import VariableBrowserModal from './modals/VariableBrowserModal.vue';
import VariableBrowser from './VariableBrowser.vue';
import CustomDropdown from './CustomDropdown.vue';
import { useOrgStore } from './../stores/organization';
import { computed } from 'vue';
const orgStore = useOrgStore();

const saveChanges = () => {
  orgStore.saveChanges().then((res) => {
    console.log(res);
  });
};

const groupVariables = (variables) => {
  //base group - only group that can be root: true
  let groups = { name: '', groups: [], variables: [], root: true };
  variables.forEach((v) => {
    let splits = v.name.replace('{', '').replace('}', '').split('_');
    let currentGroup = groups;
    console.log(splits);
    splits.forEach((split, i) => {
      // console.log(split)
      // console.log(i)

      if (i == splits.length - 1) {
        // also add a prettyName to the variable
        v.prettyName = split;
        currentGroup.variables.push(v);
      } else {
        // if the group exists - find it in the groups array if not the create it. make it the currentGroup so variables can be pushed to
        currentGroup =
          currentGroup.groups.find((g) => g.name === split) ||
          currentGroup.groups[currentGroup.groups.push({ name: split, groups: [], variables: [] }) - 1];
      }
    });
  });
  return groups;
};

const productVariables = computed(() => {
  return groupVariables(orgStore.$state.productVariables) || {};
});

const browseVariables = () => {
  $vfm.show({
    component: VariableBrowserModal,
    bind:{
      variables: productVariables.value
    } ,
    on: {
      // event by custom-modal
      confirm(close, data) {
        console.log(data);
        orgStore.createLayout(data).then((res) => {
          console.log('Created ____ ', res);
          close();
        });
      },
      cancel(close) {
        close();
      },
    },
    slots: {
      title: 'Variables',
      subtitle: orgStore.selectedProduct.title
    },
  });
};
</script>

<style></style>
