import * as types from "./mutations_type.js";

export const state = {
  restaurantList: [],
  tokenInfo: {
    account: "",
    token: ""
  }
};

export const mutations = {
  [types.GET_RESLIST](state, data) {
    state.restaurantList = data;
  },
  [types.SAVE_TOKEN](state, data) {
    state.tokenInfo = data;
  },
  [types.CLEAR_TOKEN](state) {
    state.tokenInfo.token = "";
  }
};
