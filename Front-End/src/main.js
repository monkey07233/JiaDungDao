// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from "vue";
import App from "./App";
import router from "./router";
import store from "./store";
import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import { library } from "@fortawesome/fontawesome-svg-core";
import { fas } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

Vue.config.productionTip = false;
Vue.use(BootstrapVue);
library.add(fas);
Vue.component("font-awesome-icon", FontAwesomeIcon);

router.beforeEach((to, from, next) => {
  if (
    to.matched.some(record => {
      return record.meta.requiresAuth;
    })
  ) {
    const tokenInfo = JSON.parse(localStorage.getItem("tokenInfo"));
    if (tokenInfo == null || tokenInfo.token == "") {
      next({ path: "/" });
    } else {
      next();
    }
  } else {
    next();
  }
});

/* eslint-disable no-new */
new Vue({
  el: "#app",
  router,
  store,
  components: { App },
  template: "<App/>"
});
