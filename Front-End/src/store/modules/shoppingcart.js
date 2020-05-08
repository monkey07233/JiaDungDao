import * as types from "../mutations_type.js";
import axios from "axios";

export default {
  namespaced: true,

  state: {
    shoppingCartInfo: {
      shoppingCartTotalPrice: 0,
      shoppingCartItems: []
    }
  },

  getters: {
    getShoppingCartInfo(state) {
      return state.shoppingCartInfo;
    },

    getShoppingCartTotalNum(state, getters) {
      let sum = 0;
      getters.getShoppingCartInfo.shoppingCartItems.forEach(item => {
        sum += item.o_count;
      });
      return sum;
    }
  },

  mutations: {
    [types.SAVE_SHOPPINGCART](state, item) {
      let itemInfo = item;
      let index = state.shoppingCartInfo.shoppingCartItems.findIndex(
        item =>
          (item.o_item === itemInfo.o_item) & (item.r_id === itemInfo.r_id)
      );
      if (index !== -1) {
        state.shoppingCartInfo.shoppingCartItems[index].o_count++;
      } else {
        state.shoppingCartInfo.shoppingCartItems.push(itemInfo);
        state.shoppingCartInfo.shoppingCartItems.sort(function(a, b) {
          if (a.r_id < b.r_id) return -1;
          if (a.r_id > b.r_id) return 1;
          return 0;
        });
      }
      state.shoppingCartInfo.shoppingCartTotalPrice += itemInfo.o_price;
      localStorage.setItem(
        "shpopingCart",
        JSON.stringify(state.shoppingCartInfo)
      );
    },
    [types.DELETE_SHOPPINGCART](state, items) {
      items.forEach(itemInfo => {
        let delIndex = state.shoppingCartInfo.shoppingCartItems.findIndex(
          item =>
            (item.o_item === itemInfo.o_item) & (item.r_id === itemInfo.r_id)
        );
        let delItem = state.shoppingCartInfo.shoppingCartItems[delIndex];
        state.shoppingCartInfo.shoppingCartTotalPrice -=
          delItem.o_price * delItem.o_count;
        state.shoppingCartInfo.shoppingCartItems.splice(delIndex, 1);
      });
      localStorage.setItem(
        "shpopingCart",
        JSON.stringify(state.shoppingCartInfo)
      );
    },
    [types.MINUS_NUMBER_SHOPPINGCART](state, itemInfo) {
      let index = state.shoppingCartInfo.shoppingCartItems.findIndex(
        item =>
          (item.o_item === itemInfo.o_item) & (item.r_id === itemInfo.r_id)
      );
      if (
        index !== -1 &&
        state.shoppingCartInfo.shoppingCartItems[index].o_count > 1
      ) {
        state.shoppingCartInfo.shoppingCartItems[index].o_count--;
        state.shoppingCartInfo.shoppingCartTotalPrice -= itemInfo.o_price;
      }
      localStorage.setItem(
        "shpopingCart",
        JSON.stringify(state.shoppingCartInfo)
      );
    },
    [types.SET_CART](state, item) {
      state.shoppingCartInfo = item;
    }
  },

  actions: {
    addItemToShoppingCart(context, order) {
      return new Promise((resolve, reject) => {
        context.commit(types.SAVE_SHOPPINGCART, order);
        resolve();
      });
    },
    setCart({ commit }, cart) {
      commit(types.SET_CART, cart);
    },

    deleteItemFromCart({ commit }, item) {
      commit(types.DELETE_SHOPPINGCART, item);
    },

    minusItemToShoppingCart({ commit }, order) {
      commit(types.MINUS_NUMBER_SHOPPINGCART, order);
    }
  }
};
