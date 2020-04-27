import { state } from "./mutations";

export const getResList = state => {
    return state.restaurantList;
};

export const getResInfo = state => {
    return state.restaurantInfo;
};

export const getMemberInfo = state => {
    return state.memberInfo;
};

export const getTokenInfo = state => {
    return state.tokenInfo;
};

export const getResByAcc = (state, getters) => {
    return state.restaurantList.filter(res => res.m_account === getters.getTokenInfo.account);
};

export const getShoppingCartInfo = state => {
    if (JSON.parse(localStorage.getItem("shpopingCart")) != null) {
        return state.shoppingCartInfo=JSON.parse(localStorage.getItem("shpopingCart"));
    } else {
        return state.shoppingCartInfo;
    }
};

export const getShoppingCartTotalNum = (state, getters) => {
    let sum = 0;
    getters.getShoppingCartInfo.shoppingCartItems.forEach(item => {
        sum += item.number;
    });
    return sum;
};