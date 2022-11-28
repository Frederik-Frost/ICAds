<template>
  <main class="h-screen bg-offWhite flex flex-col justify-center align-middle">
    <div class="m-auto">
      <img src="./../assets/svg/adslogo.svg" alt="IC Ads logo" class="w-12 m-auto my-8" />
      <form
        @submit.prevent="login()"
        class="flex flex-col justify-center align-middle w-[400px] drop-shadow p-6 bg-white gap-3 rounded-md"
      >
        <h1 class="text-2xl">Login</h1>

        <input type="text" placeholder="email" v-model="credentials.email" />
        <input type="password" placeholder="password" v-model="credentials.password" />
        <span class="text-flame">{{ errorMessage }}</span>
        <button type="submit" class="btn-primary">Login</button>
      </form>
      <p class="text-center m-auto my-2">No account? <a href="#" class="link" >Signup</a></p>
    </div>
  </main>
</template>
<script>
import { defineComponent, ref, reactive } from 'vue';
import { useMainStore } from './../stores/main';
export default defineComponent({
  setup() {
    const store = useMainStore();
    const credentials = ref({
      email: '',
      password: '',
    });
    let errorMessage = ref('');

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
    return {
      errorMessage,
      credentials,
      login,
      store,
    };
  },
});
</script>
