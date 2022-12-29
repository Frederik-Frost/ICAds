<template>
  <div class="max-w-screen-xl mx-auto mt-4 px-4">
    <h1 class="text-4xl mb-4">This is your account overview</h1>
    <main>
      <!-- Current user info here -->
      <section class="bg-white shadow-md rounded p-4 mb-4">
        <form @submit.prevent="updateUser()" class="flex flex-col gap-2">
          <legend class="text-lg font-bold text-charcoal">User info</legend>
          <CustomInput type="text" name="userEmail" label="Email" v-model="user.email" disabled />
          <div class="flex flex-col gap-2 sm:flex-row">
            <CustomInput type="text" name="userFirstname" label="First Name" v-model="user.firstname" class="sm:flex-1"/>
            <CustomInput type="text" name="userLastname" label="Last Name" v-model="user.lastname" class="sm:flex-1"/>
          </div>
          <button class="btn-primary" type="submit">Update user</button>
        </form>
      </section>
      <section class="bg-white shadow-md rounded p-4 mb-4">
        <!-- organization info here -->
        <form @submit.prevent="updateOrganization()" class="flex flex-col gap-2">
          <legend class="text-lg font-bold text-charcoal">Organization info</legend>
          <div class="flex flex-col gap-2 sm:flex-row items-end">
            <CustomInput type="text" name="orgName" label="Organization name" v-model="organization.name" class="sm:flex-auto"/>
            <button class="btn-primary h-[38px]" type="submit">Update Organization</button>
          </div>
        </form>
      </section>
      <section class="bg-white shadow-md rounded p-4 mb-4">
        <div class="flex flex-col sm:flex-row sm:justify-between sm:items-center">
          <p class="text-lg font-bold text-charcoal">Organization users</p>
          <div class="flex flex-row gap-4 items-center">
            <p>{{ orgUsersMessage }}</p>
            <button
              class="btn-secondary"
              @click="onAddUser"
              :disabled="!user.isAdmin"
              :tooltip="!user.isAdmin ? 'Only admins can add users' : ''"
              tooltip-placement="top-left"
            >
              + User
            </button>
          </div>
        </div>

        <div>
          <header class="w-full grid grid-cols-2 sm:grid-cols-4 py-2">
            <span class="font-bold">Name</span>
            <span class="font-bold">Email</span>
            <span class="font-bold hidden sm:block">Type</span>
            <span class="font-bold hidden sm:block">Status</span>
          </header>
          <div v-for="(user, index) in orgStore.organizationUsers" :key="index" class="w-full grid grid-cols-2 sm:grid-cols-4 py-2">
            <span>{{ `${user.firstname} ${user.lastname}` }}</span>
            <span>{{ user.email }}</span>
            <span class="hidden sm:block">{{ user.isAdmin ? 'Administrator' : 'Standard' }}</span>
            <span class="hidden sm:block">{{ user.active ? 'Active' : 'Not active' }}</span>
          </div>
        </div>
      </section>
      <!-- organization users here -->
    </main>
  </div>
</template>
<script setup>
import CustomInput from './../components/CustomInput.vue';
import CreateUserModal from './../components/modals/CreateUserModal.vue';
import { $vfm } from 'vue-final-modal';
import { useUserStore } from './../stores/user';
import { useOrgStore } from '../stores/organization';
import { ref, onMounted, defineProps, computed, watch } from 'vue';
const userStore = useUserStore();
const orgStore = useOrgStore();
const user = ref({});
const organization = ref({});
const userChanges = ref(false);

const orgUsersMessage = computed(() => {
  const count = orgStore.organizationUsers.length;
  const users = count == 1 ? 'user' : 'users';
  return `${count} ${users} in ${orgStore.organization.name}`;
});
onMounted(() => {
  user.value = { ...userStore.user };
  organization.value = { ...orgStore.organization };
  getOrgUsers();
});
const updateUser = () => {
  console.log(user.value);
  userStore.updateUserInfo(user.value);
};
const updateOrganization = () => {
  console.log(organization.value);
  orgStore.updateOrganizationInfo(organization.value);
};
const getOrgUsers = () => {
  orgStore.getOrganizationUsers();
};

const onAddUser = () => {
  $vfm.show({
    component: CreateUserModal,
    on: {
      confirm(close, data) {
        orgStore.createOrganizationUser(data).then(() => {
          close();
        });
      },
      cancel(close) {
        close();
      },
    },
    slots: {
      title: 'Create user',
    },
  });
};
</script>
<style></style>
