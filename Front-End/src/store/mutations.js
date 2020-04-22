import * as types from "./mutations_type.js";

export const state = {
    shoppingCartItems: [],
    restaurantList: [],
    tokenInfo: {
        account: "",
        token: ""
    },
    memberInfo: {
        m_name: "",
        m_birthday: "",
        m_email: "",
        m_address: ""
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
    },
    [types.GET_MEMBERINFO](state, data) {
        state.memberInfo = data;
        var birthday = data.m_birthday.split("T");
        state.memberInfo.m_birthday = birthday[0];
    },
    [types.SAVE_SHOPPINGCART](state, newItem) {
        state.shoppingCartItems.push(newItem)
    }
};