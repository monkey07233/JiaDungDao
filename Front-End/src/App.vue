<template>
  <div id="app">
    <navbar />
    <div id="content">
      <router-view />
    </div>
  </div>
</template>

<script>
import "@/assets/css/layout.css";
import navbar from "./components/Navbar.vue";
export default {
  name: "App",
  components: {
    navbar
  },
  created() {
    const tokenInfo = JSON.parse(localStorage.getItem("tokenInfo"));
    if (tokenInfo != null) {
      this.$store.dispatch("checkToken", tokenInfo);
    }
  }
};
</script>

<style>
body {
  font-family: "Microsoft JhengHei", sans-serif, monospace;
}
</style>
