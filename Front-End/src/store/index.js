import Vue from "vue";
import Vuex from "vuex";
// import { state, mutations } from "./mutations.js";
// import * as getters from "./getters.js";
// import * as actions from "./actions.js";
import * as state from "./state.js";
import restaurant from "./modules/restaurant";
import menu from "./modules/menu";
import shoppingcart from "./modules/shoppingcart";
import member from "./modules/member";
import order from "./modules/order";

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        restaurant,
        shoppingcart,
        menu,
        order,
        member
    },
    state,
    mutations: {},
    getters: {},
    actions: {},
    strict: true
});