import * as types from "./mutations_type.js";

export const state = {
  OrderInfo: [{
    title: {
      OrderId: "",
      RestaurantID: "",
      r_name: "",
      m_account: "",
      o_createtime: "",
      o_total: 0
    },
    orderDetail: []
  }],
  shoppingCartInfo: {
    shoppingCartTotalPrice: 0,
    shoppingCartItems: [],
  },
  restaurantList: [],
  tokenInfo: {
    account: "",
    token: "",
    role: 0
  },
  memberInfo: {
    MemberId: "",
    m_name: "",
    m_birthday: "",
    m_email: "",
    m_address: "",
    m_imgUrl: ""
  },
  restaurantInfo: {
    restaurant: {
      restaurantID: "",
      r_name: "",
      r_address: "",
      r_tel: ""
    },
    typeAndMenu: []
  }
};

export const mutations = {
  [types.GET_RESLIST](state, data) {
    state.restaurantList = data;
  },
  [types.GET_RESINFO](state, data) {
    state.restaurantInfo = data;
  },
  [types.SAVE_TOKEN](state, data) {
    state.tokenInfo = data;
  },
  [types.CLEAR_TOKEN](state) {
    state.tokenInfo.token = "";
    state.tokenInfo.account = "";
    state.tokenInfo.role = 0;
  },
  [types.GET_MEMBERINFO](state, data) {
    state.memberInfo = data;
    var birthday = data.m_birthday.split("T");
    state.memberInfo.m_birthday = birthday[0];
  },
  [types.SAVE_SHOPPINGCART](state, item) {
    let itemInfo = JSON.parse(JSON.stringify(item));
    let index = state.shoppingCartInfo.shoppingCartItems.findIndex(
      item => item.o_item === itemInfo.o_item & item.r_id === itemInfo.r_id
    );
    if (index !== -1) {
      state.shoppingCartInfo.shoppingCartItems[index].o_count++;
    } else {
      state.shoppingCartInfo.shoppingCartItems.push(itemInfo);
      state.shoppingCartInfo.shoppingCartItems.sort(function (a, b) {
        if (a.r_id < b.r_id) return -1;
        if (a.r_id > b.r_id) return 1;
        return 0;
      });
    }
    state.shoppingCartInfo.shoppingCartTotalPrice += itemInfo.o_price;
    localStorage.setItem("shpopingCart", JSON.stringify(state.shoppingCartInfo));
  },
  [types.DELETE_SHOPPINGCART](state, items) {
    items.forEach(itemInfo => {
      let delIndex = state.shoppingCartInfo.shoppingCartItems.findIndex(
        item => item.o_item === itemInfo.o_item & item.r_id === itemInfo.r_id
      );
      let delItem = state.shoppingCartInfo.shoppingCartItems[delIndex];
      state.shoppingCartInfo.shoppingCartTotalPrice -= delItem.o_price * delItem.o_count;
      state.shoppingCartInfo.shoppingCartItems.splice(delIndex, 1);
    });
    localStorage.setItem("shpopingCart", JSON.stringify(state.shoppingCartInfo));
  },
  [types.MINUS_NUMBER_SHOPPINGCART](state, item) {
    let itemInfo = JSON.parse(JSON.stringify(item));
    let index = state.shoppingCartInfo.shoppingCartItems.findIndex(
      item => item.o_item === itemInfo.o_item & item.r_id === itemInfo.r_id
    );
    if (
      index !== -1 &&
      state.shoppingCartInfo.shoppingCartItems[index].o_count > 1
    ) {
      state.shoppingCartInfo.shoppingCartItems[index].o_count--;
      state.shoppingCartInfo.shoppingCartTotalPrice -= itemInfo.o_price;
    }
    localStorage.setItem("shpopingCart", JSON.stringify(state.shoppingCartInfo));
  },
  [types.GET_ORDER](state, order) {
    let orderinfo = JSON.parse(JSON.stringify(order));
    state.OrderInfo = orderinfo;
  },
  [types.SET_CART](state, item) {
    state.shoppingCartInfo = item;
  },
};
