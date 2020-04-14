import * as types from "./mutations_type.js";

export const state = {
  restaurantList: [],
  registerMessage:''
};

export const mutations = {
  [types.GET_RESLIST](state, data) {
    state.restaurantList = data;
  },
  [types.REGISTER_MESSAGE](state, message){
    state.registerMessage = message;
  }
};
