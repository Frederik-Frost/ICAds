<template>
  <main class="max-w-screen-xl mx-auto mt-4 px-4">
    <div class="flex justify-between items-end mb-4">
      <div>
        <h1 class="text-4xl">Hey {{ user.firstname }}</h1>
        <p class="text-lg font-bold">Here are your layouts</p>
      </div>

      <div class="flex flex-row gap-x-2">
        <button class="btn-secondary" @click="sidepanel = true">+ Data</button>
        <button class="btn-primary" @click="onAddNewLayout()">+ Layout</button>
      </div>
    </div>
    <LayoutList />
    <SidePanel v-if="sidepanel" @close="sidepanel = false">
      <template #content>
        <div class="p-4">
          <header class="border-b border-b-charcoal50 pb-2 flex flex-row justify-between">
            <h2>Data</h2>
            <button @click="sidepanel = false" class="opacity-50 hover:opacity-100">
              <span class="material-symbols-outlined"> close </span>
            </button>
          </header>
          <main>
            <article
              v-for="(integration, index) in orgStore.integrations"
              :key="integration.id"
              class="flex flex-col gap-2 border border-charcoal25 mt-4 p-2 rounded-md"
            >
              <div class="form-group">
                <label :for="`integrationName${index}`">Name</label>
                <input type="text" :name="`integrationName${index}`" v-model="integration.name" />
              </div>
              <div class="form-group">
                <label :for="`integrationUrl${index}`">Url</label>
                <input type="text" :name="`integrationUrl${index}`" v-model="integration.url" />
              </div>
              <div class="form-group">
                <label :for="`integrationToken${index}`">Access token</label>
                <input type="text" :name="`integrationToken${index}`" v-model="integration.accessToken" />
              </div>

              <div class="self-end flex gap-2">
                <button class="btn-danger" @click="deleteIntegration(integration.id, index)">Delete</button>
                <button class="btn-secondary" @click="updateIntegration(integration, index)">Save</button>
              </div>
            </article>

            <button class="btn-tertiary mt-3" @click="addIntegration()">+ Add</button>
          </main>
        </div>
      </template>
    </SidePanel>
  </main>
</template>
<script setup>
import { VueFinalModal } from 'vue-final-modal';
import LayoutList from './../components/LayoutList.vue';
import SidePanel from './../components/SidePanel.vue';
import AddLayoutModal from '../components/modals/AddLayoutModal.vue';
import { computed, reactive, defineComponent, onMounted, ref, inject } from 'vue';
import { useMainStore } from './../stores/main';
import { useUserStore } from './../stores/user';
import { useOrgStore } from '../stores/organization';
import { useRouter } from 'vue-router';
import { $vfm } from 'vue-final-modal';
const router = useRouter();
const main = useMainStore();
const userStore = useUserStore();
const orgStore = useOrgStore();
const user = ref(userStore.user);
const showModal = ref(false);
const show = ref(false);
const sidepanel = ref(false);

onMounted(() => {
  // load layouts and integrations
  Promise.all([orgStore.getLayouts(), orgStore.getIntegrations()]).then((values) => {
    console.log(values);
  });
});

const addIntegration = () => {
  orgStore.$patch((state) => {
    state.integrations.push({ name: '', url: '', accessToken: '', newIntegration: true });
  });
};

const updateIntegration = (integration, index) => {
  if (integration.newIntegration === true) {
    let request = { ...integration };
    delete request.newIntegration;
    console.log(request);
    orgStore.createIntegration(request).then((res) => {
      patchIntegrations(res, index);
    });
  } else {
    orgStore.updateIntegration(integration).then((res) => {
      patchIntegrations(res, index);
    });
  }
  console.log(integration);
};
const patchIntegrations = (update, index) => {
  console.log('PATCHING ___ ', update, index);
  orgStore.$patch((state) => {
    state.integrations[index] = update;
  });
};

const deleteIntegration = (id, index) => {
  orgStore.deleteIntegration(id).then((res) => {
    console.log(res);
    orgStore.$patch((state) => {
      state.integrations.splice(index, 1);
    });
  });
};

const onAddNewLayout = () => {
  $vfm.show({
    component: AddLayoutModal,
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
      title: 'New Layout',
    },
  });
};

const logout = () => {
  main.logout();
};
</script>
