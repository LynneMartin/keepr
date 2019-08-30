<template>
  <div class="home">
    <h1>Welcome Home {{user.username}}</h1>
    <!--NOTE If logged in, show logout button. If not logged in, show login button-->
    <button v-if="user.id" @click="logout">logout</button>
    <router-link v-else :to="{name: 'login'}">Login</router-link>
  </div>
</template>

<script>
export default {
  name: "home",
  mounted() {
    this.$store.dispatch('getPublicKeepsComponent')
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: {
    publicKeepsComponent
  }
};
</script>