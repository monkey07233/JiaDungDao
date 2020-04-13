<template>
  <div id="app">
    <narbar />
    <div id="content">
      <router-view />
    </div>
  </div>
</template>

<script>
import "@/assets/css/layout.css";
import narbar from "./components/Navbar.vue";
export default {
  name: "App",
  components: {
    narbar
  }
};
</script>

<style>
#app {
  font-family: "Microsoft JhengHei",sans-serif,monospace;
}
</style>
