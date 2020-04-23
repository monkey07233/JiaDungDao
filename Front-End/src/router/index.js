import Vue from "vue";
import Router from "vue-router";
import Index from "@/pages/Index";
import Profile from "@/pages/profile";
import Cart from "@/pages/Cart";
import Restaurant from "@/pages/restaurant";
import RestaurantManagement from "@/pages/RestaurantManagement";
import AddRestaurant from "@/pages/AddRestaurant";
import store from "../store";
import RestaurantEdit from "@/pages/RestaurantEdit"
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
      component: Restaurant
    },
    {
      path: "/Cart",
      name: "Cart",
      component: Cart,
      meta: { requiresAuth: true }
    },
    {
      path: "/RestaurantManagement",
      name: "RestaurantManagement",
      component: RestaurantManagement,
      meta: { requiresAuth: true },
      beforeEnter(to, from, next) {
        const role = JSON.parse(JSON.stringify(store.state.tokenInfo)).role;
        if (role != 0) {
          next();
        }
      }    
    },
    {
      path: "/RestaurantEdit",
      name: "RestaurantEdit",
      component: RestaurantEdit,
      meta: { requiresAuth: true }
    },       
    {
      path: "/RestaurantManagement/AddRestaurant",
      name: "AddRestaurant",
      component: AddRestaurant
    }
  ]
});
