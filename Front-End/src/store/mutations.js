import * as types from "./mutations_type.js";

export const state = {
  restaurantList: [],
  memberName:"",
  tokenInfo: {
    account: "",
    token: ""
  },
  memberInfo:{
    m_name: "",
    m_birthday: "",
    m_email: "",
    m_address: ""
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
  },
  [types.GET_MEMBERINFO](state,data){
    state.memberInfo = data;
    var birthday = data.m_birthday.split('T');
    state.memberInfo.m_birthday = birthday[0];
  },
  [types.GET_NAME](state,name){
    state.memberName = name;
  }
};
