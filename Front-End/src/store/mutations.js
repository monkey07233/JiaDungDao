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
        state.tokenInfo.role = 0;
    },
    [types.GET_MEMBERINFO](state, data) {
        state.memberInfo = data;
        var birthday = data.m_birthday.split("T");
        state.memberInfo.m_birthday = birthday[0];
    },
    [types.SAVE_SHOPPINGCART](state, item) {
        let itemInfo = JSON.parse(JSON.stringify(item));
        let index = state.shoppingCartInfo.shoppingCartItems.findIndex(item => item.o_item === itemInfo.o_item);
        if (index !== -1) {
            state.shoppingCartInfo.shoppingCartItems[index].o_count++;
        } else {
            state.shoppingCartInfo.shoppingCartItems.push(itemInfo);
        }
        state.shoppingCartInfo.shoppingCartTotalPrice += itemInfo.o_price;
    },
    [types.MINUS_NUMBER_SHOPPINGCART](state, item) {
        let itemInfo = JSON.parse(JSON.stringify(item));
        let index = state.shoppingCartInfo.shoppingCartItems.findIndex(item => item.o_item === itemInfo.o_item);
        if (index !== -1 && cartItem.number > 1) {
            state.shoppingCartInfo.shoppingCartItems[index].o_count--;
            state.shoppingCartInfo.shoppingCartTotalPrice -= itemInfo.o_price;
        }
    },
    [types.GET_ORDER](state, order) {
        let orderinfo = JSON.parse(JSON.stringify(order));
        state.OrderInfo = orderinfo;
    }
};