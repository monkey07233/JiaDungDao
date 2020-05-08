import * as types from "../mutations_type.js";
import axios from "axios";

export default {
  namespaced: true,

  state: {
    orderInfo: [
      {
        title: {
          OrderId: "",
          RestaurantID: "",
          r_name: "",
          m_account: "",
          o_createtime: "",
          o_total: 0
        },
        orderDetail: []
      }
    ]
  },

  getters: {
    getOrderInfo(state) {
      return state.orderInfo;
    }
  },

  mutations: {
    [types.GET_ORDER](state, order) {
      state.orderInfo = order;
    }
  },

  actions: {
    createOrder({ commit, state }, orderInfo) {
      const config = {
        withCredentials: true,
        headers: {
          Authorization: "Bearer " + JSON.parse(localStorage.tokenInfo).token
        }
      };
      return new Promise((resolve, reject) => {
        axios
          .post(
            "https://localhost:5001/api/Order/CreateOrder",
            orderInfo,
            config
          )
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
          Authorization: "Bearer " + JSON.parse(localStorage.tokenInfo).token
        }
      };
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

    getOrderInfoByResId({ commit, state }, resId) {
      const config = {
        withCredentials: true,
        headers: {
          Authorization: "Bearer " + JSON.parse(localStorage.tokenInfo).token
        }
      };
      return new Promise((resolve, reject) => {
        axios
          .get(
            "https://localhost:5001/api/Order/GetOrderInfoByResId?restaurantId=" +
              resId,
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
    }
  }
};
