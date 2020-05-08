import * as types from "../mutations_type.js";
import axios from "axios";

export default {
    namespaced: true,

    state: {

    },

    getters: {

    },

    mutations: {

    },

    actions: {
        addMenuItem({ commit, state }, newMenuItem) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + JSON.parse(localStorage.tokenInfo).token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .post(
                        "https://localhost:5001/api/Restaurant/AddMenuItem",
                        JSON.parse(JSON.stringify(newMenuItem)),
                        config
                    )
                    .then(function() {
                        resolve();
                    })
                    .catch(function(err) {
                        console.log(err);
                        reject();
                    });
            });
        },

        deleteMenuItem({ commit, state }, menuID) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + JSON.parse(localStorage.tokenInfo).token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .get(
                        "https://localhost:5001/api/Restaurant/DeleteMenu?MenuID=" + menuID,
                        config
                    )
                    .then(function() {
                        resolve();
                    })
                    .catch(function(err) {
                        console.log(err);
                        reject();
                    });
            });
        },

        updateMenu({ commit, state }, menu) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + JSON.parse(localStorage.tokenInfo).token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .post("https://localhost:5001/api/Restaurant/updateMenu", menu, config)
                    .then(function(res) {
                        resolve(res.data);
                    })
                    .catch(function(err) {
                        console.log(err);
                        reject();
                    });
            });
        },
        getMenuId({ commit, state }) {
            const config = {
                withCredentials: true,
                headers: {
                    Authorization: "Bearer " + JSON.parse(localStorage.tokenInfo).token
                }
            };
            return new Promise((resolve, reject) => {
                axios
                    .get("https://localhost:5001/api/Restaurant/GetLatestMenuId", config)
                    .then(function(res) {
                        resolve(res);
                    })
                    .catch(function(err) {
                        console.log(err);
                        reject();
                    });
            });
        },
    }
}