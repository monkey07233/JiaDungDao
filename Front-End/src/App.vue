<template>
  <div id="app">
    <narbar />
    <router-view/>
  </div>
</template>

<script>
import "@/assets/css/layout.css";
import narbar from './components/Navbar.vue'
export default {
  name: 'App',
  components:{
    narbar
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
}
</style>
