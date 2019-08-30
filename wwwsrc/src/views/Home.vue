<template>
  <div class="home container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-12">
        <h1>Welcome To Keepr, {{user.username}}</h1>
        <!--NOTE If logged in, show logout button. If not logged in, show login button-->
        <!--TODO Make a new row/column for login/logout button-->
        <button v-if="user.id" @click="logout">logout</button>
        <router-link v-else :to="{name: 'login'}">Login</router-link>
      </div>
      <publicKeeps></publicKeeps>
    </div>
  </div>
</template>

<script>
  //import publicKeeps from '../Components/PublicKeeps.vue'
export default {
  name: "home",
  mounted() {
    this.$store.dispatch('getPublicKeeps')
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: {
    
  }
};
</script>