<template>
  <div class="max-w-screen-xl mx-auto mt-4 px-4">
    <h1>This is an account page</h1>
    <main>
      <!-- Current user info here -->
      <section>
        <form @submit.prevent="updateUser()">
          <legend class="text-lg font-bold text-charcoal">User info</legend>
          <CustomInput type="text" name="userEmail" label="Email" v-model="user.email" disabled />
          <CustomInput type="text" name="userFirstname" label="First Name" v-model="user.firstname" />
          <CustomInput type="text" name="userLastname" label="Last Name" v-model="user.lastname" />
          <button class="btn-primary" type="submit">Update user</button>
        </form>
      </section>
      <section>
        <!-- organization info here -->
        <form @submit.prevent="updateOrganization()">
          <legend class="text-lg font-bold text-charcoal">Organization info</legend>
          <CustomInput type="text" name="orgName" label="Organization name" v-model="organization.name" />
          <button class="btn-primary" type="submit">Update Organization</button>
        </form>
      </section>
      <section>
        <div class="flex flex-row justify-between items-center">
          <p class="text-lg font-bold text-charcoal">Organization users</p>
          <div class="flex flex-row gap-4 items-center">
            <p>{{ orgUsersMessage }}</p>
            <button class="btn-secondary">+ User</button>
          </div>
        </div>

        <div>
          <header class="w-full grid grid-cols-4 py-2">
            <span class="font-bold">Name</span>
            <span class="font-bold">Email</span>
            <span class="font-bold">Type</span>
            <span class="font-bold">Status</span>
          </header>
          <div v-for="(user, index) in orgStore.organizationUsers" :key="index" class="w-full grid grid-cols-4 py-2">
            <span>{{ `${user.firstname} ${user.lastname}` }}</span>
            <span>{{ user.email }}</span>
            <span>{{user.isAdmin ? 'Administrator' : 'Standard'}}</span>
            <span>{{user.active ? 'Active' : 'Not active'}}</span>
            
          </div>
        </div>
      </section>
      <!-- organization users here -->
    </main>
  </div>
</template>
<script setup>
import CustomInput from './../components/CustomInput.vue';
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
  return `${count} ${users} in ${organization.value.name}`;
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
</script>
<style></style>
