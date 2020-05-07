import * as types from "../mutations_type.js";
import axios from "axios";

export default {
    namespaced: true,

    state: {
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
        }]
    },

    getters: {
        getOrderInfo(state) {
            return state.OrderInfo;
        }
    },

    mutations: {
        [types.GET_ORDER](state, order) {
            let orderinfo = JSON.parse(JSON.stringify(order));
            state.OrderInfo = orderinfo;
        }
    },

    actions: {
        createOrder({ commit, state }, orderInfo) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + state.tokenInfo.token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .post("https://localhost:5001/api/Order/createOrder", orderInfo, config)
                    .then(function(res) {
                        resolve(res.data);
                    })
                    .catch(function(err) {
                        console.log(err);
                        reject();
                    });
            });
        },
        getOrderInfo({ commit, state }, order) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + state.tokenInfo.token
                }
            };
            console.log(config);
            return new Promise((resolve, reject) => {
                axios
                    .post("https://localhost:5001/api/Order/GetOrderInfo", order, config)
                    .then(function(res) {
                        commit(types.GET_ORDER, res.data);
                        resolve(res.data);
                    })
                    .catch(function(err) {
                        console.log(err);
                        reject();
                    });
            });
        },

        getOrderInfoByResId({ commit, state }, ResId) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + state.tokenInfo.token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .get(
                        "https://localhost:5001/api/Order/GetOrderInfoByResId?restaurantId=" +
                        ResId,
                        config
                    )
                    .then(function(res) {
                        commit(types.GET_ORDER, res.data);
                        resolve(res.data);
                    })
                    .catch(function(err) {
                        console.log(err);
                        reject();
                    });
            });
        },
    }
}