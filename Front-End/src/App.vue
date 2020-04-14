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
  }
};
</script>

<style>
body {
  font-family: "Microsoft JhengHei",sans-serif,monospace;
}
</style>
