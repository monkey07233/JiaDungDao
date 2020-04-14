import * as types from "./mutations_type.js";

export const state = {
  restaurantList: []
};

export const mutations = {
  [types.GET_RESLIST](state, data) {
    state.restaurantList = data;
  }
};
