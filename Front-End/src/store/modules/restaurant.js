import * as types from "../mutations_type.js";
import axios from "axios";

export default {
    namespaced: true,

    state: {
        restaurantList: [],
        restaurantInfo: {
            restaurant: {
                restaurantID: "",
                r_name: "",
                r_address: "",
                r_tel: ""
            },
            typeAndMenu: []
        }
    },

    getters: {
        getResList(state) {
            return state.restaurantList;
        },
        getResInfo(state) {
            return state.restaurantInfo;
        },
        getResByAcc(state, getters) {
            return state.restaurantList.filter(res => res.m_account === getters.getTokenInfo.account);
        }
    },

    mutations: {
        [types.GET_RESLIST](state, data) {
            state.restaurantList = data;
        },
        [types.GET_RESINFO](state, data) {
            state.restaurantInfo = data;
        },
    },

    actions: {
        getRestaurantList({ commit }) {
            axios
                .get("https://localhost:5001/api/Restaurant/GetAllRestaurant")
                .then(function(res) {
                    commit(types.GET_RESLIST, res.data);
                })
                .catch(function(err) {
                    console.log(err);
                });
        },

        getRestaurantInfo({ commit }, id) {
            axios
                .get("https://localhost:5001/api/Restaurant/GetRestaurantInfoById?id=" + id)
                .then(function(res) {
                    commit(types.GET_RESINFO, res.data);
                })
                .catch(function(err) {
                    console.log(err);
                });
        },

        createRestaurant({ commit, state }, newRestaurant) {
            console.log(newRestaurant);
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + state.tokenInfo.token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .post(
                        "https://localhost:5001/api/Restaurant/createRestaurant",
                        newRestaurant,
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
        UpdateRestaurant({ commit, state }, restaurant) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + state.tokenInfo.token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .post(
                        "https://localhost:5001/api/Restaurant/updateRestaurant",
                        restaurant,
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
        deleteRestaurant({ commit, state }, restaurantID) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + state.tokenInfo.token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .get(
                        "https://localhost:5001/api/Restaurant/DeleteRestaurant?RestaurantID=" +
                        restaurantID,
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
    }
}