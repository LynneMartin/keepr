<!-- NOTE Users have a dashboard to see all their keeps and vaults (logged in?) -->

<template>
  <div class="Vault View">
    <div class="row">
      <div class="col-12 justify-content-center">
        <h1>WELCOME TO YOUR VAULT</h1>
  <!--<div class="row"></div>-->
  <!--<div class="col-12"></div>-->
        <h2>Vault Name: {{activeVault.name}}</h2> <!--Store-->
        <h3>Description: {{activeVault.description}}</h3>
        <!--<div class="row"></div>-->
        <!--<div class="col-12 justify-content-center></div>-->
<!--<h2 class="bg-secondary">Your Collected Keeps</h2>-->
<!--<div class="m-3" v-for="vaultKeep in vaultKeeps" :key="vaultKeeps></div>-->
<!--<h2>{{vaultKeeps.name}}</h2>-->
<!--<h3>{{vaultKeeps.description}}</h3>-->
<!--<button class="btn btn-primary">Delete Keep</button>-->
      </div>
    </div>
  </div>
</template>

<script>
  //importing anything from components?
  import router from '../router'
  export default {
    name: "vault",
    data() {
      return {};
    },
    mounted() {
      this.$store.dispatch("getVaultByUserId", this.vaultId);
      this.$store.dispatch("getVaultKeepsById", this.vaultId);
    }
  }
</script>

<!--STUB Come back to this -->