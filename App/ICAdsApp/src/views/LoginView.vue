<template>
  <main class="h-screen bg-offWhite flex flex-col justify-center align-middle">
    <div class="m-auto" v-if="!isSignupView">
      <img src="./../assets/svg/icadslogotext.svg" alt="IC Ads logo" class="w-[150px] m-auto my-8" />
      <form
        @submit.prevent="login()"
        class="flex flex-col justify-center align-middle w-[400px] drop-shadow p-6 bg-white gap-3 rounded-md"
      >
        <h1 class="text-2xl">Login</h1>
        <p class="text-success">{{ successMessage }}</p>
        <CustomInput type="text" label="email" v-model="credentials.email" />
        <CustomInput type="password" label="password" v-model="credentials.password" />
        <span class="text-flame">{{ errorMessage }}</span>
        <button type="submit" class="btn-primary self-end">Login</button>
      </form>
      <p class="text-center m-auto my-2">
        No account? <a href="#" class="link" @click="isSignupView = !isSignupView">Signup</a>
      </p>
    </div>
    <div class="m-auto" v-else>
      <img src="./../assets/svg/icadslogotext.svg" alt="IC Ads logo" class="w-[150px] m-auto my-8" />
      <form
        @submit.prevent="signup()"
        class="flex flex-col justify-center align-middle w-[400px] drop-shadow p-6 bg-white gap-3 rounded-md"
      >
        <h1 class="text-2xl">Signup</h1>
        <p class="text-charcoal75">Create an organization and start creating effective layouts for your ad campaigns</p>
        <CustomInput type="text" label="Organization name" v-model="newUser.organizationName" />
        <p class="text-charcoal75">And some info on you</p>
        <CustomInput type="text" label="email" v-model="newUser.user.email" />
        <CustomInput type="text" label="firstname" v-model="newUser.user.firstname" />
        <CustomInput type="text" label="lastname" v-model="newUser.user.lastname" />
        <CustomInput type="password" label="password" v-model="newUser.user.password"/>
        <span class="text-flame">{{ errorMessage }}</span>
        <button type="submit" class="btn-primary self-end" :disabled="!signupIsValid">Sign up</button>
      </form>
      <p class="text-center m-auto my-2">
        Have an account? <a href="#" class="link" @click="isSignupView = !isSignupView">Login</a>
      </p>
    </div>
  </main>
</template>
<script setup>
import CustomInput from './../components/CustomInput.vue';
import { defineComponent, ref, reactive, computed, watch } from 'vue';
import { useMainStore } from './../stores/main';
const store = useMainStore();
const isSignupView = ref(false);
const errorMessage = ref('');
const successMessage = ref('');
watch(
  () => isSignupView.value,
  () => {
    errorMessage.value = '';
    successMessage.value = '';
  }
);

// Login
const credentials = ref({
  email: '',
  password: '',
});
const login = () => {
  errorMessage.value = '';
  store
    .login(credentials.value)
    .then((res) => {
      console.log(res);
    })
    .catch((e) => {
      console.log(e);
      errorMessage.value = e;
    });
};

// Signup
const newUser = ref({
  organizationName: '',
  user: {
    firstname: '',
    lastname: '',
    email: '',
    password: '',
  },
});

const signupIsValid = computed(() => {
  return newUser.value.organizationName.length > 0 && Object.values(newUser.value.user).every((v) => v.length > 0);
});

const signup = () => {
  errorMessage.value = '';
  store
    .signup(newUser.value)
    .then((res) => {
      errorMessage.value = '';
      credentials.value.email = res.data.email;
      isSignupView.value = false;
      successMessage.value = `Welcome ${res.data.firstname}! Login and let your creative skills loose!`;
    })
    .catch((e) => {
      errorMessage.value = e.response.data;
    });
};
</script>
