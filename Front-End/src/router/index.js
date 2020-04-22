import Vue from "vue";
import Router from "vue-router";
import Index from "@/pages/Index";
import Profile from "@/pages/profile";
import Cart from "@/pages/Cart"
import Restaurant from "@/pages/restaurant"

Vue.use(Router);

export default new Router({
  mode: "history",
  routes: [
    {
      path: "*",
      redirect: "/"
    },
    {
      path: "/",
      name: "Index",
      component: Index
    },
    {
      path: "/Profile",
      name: "Profile",
      component: Profile,
      meta: { requiresAuth: true }
    },
    {
      path: "/Restaurant/:id",
      name: "Restaurant",
      component: Restaurant,
    },
    {
      path: "/Cart",
      name: "Cart",
      component: Cart,
      meta: { requiresAuth: true }
    },
  ]
});
