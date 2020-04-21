import Vue from "vue";
import Router from "vue-router";
import Index from "@/pages/Index";
import Profile from "@/pages/profile";
import RestaurantInfo from "@/pages/restaurant"
import Cart from "@/pages/Cart"

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
      path: "/RestaurantInfo",
      name: "RestaurantInfo",
      component: RestaurantInfo,
    },
    {
      path: "/Cart",
      name: "Cart",
      component: Cart,
      meta: { requiresAuth: true }
    },
  ]
});