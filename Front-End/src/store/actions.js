import * as types from "./mutations_type.js";
import axios from "axios";

export const getRestaurantList = ({ commit }) => {
  axios
    .get("https://localhost:5001/api/Restaurant/GetAllRestaurant")
    .then(function(res) {
      commit(types.GET_RESLIST, res.data);
    })
    .catch(function(err) {
      console.log(err);
    });
};

export const register = ({ commit }, newMember) => {
  return new Promise((resolve, reject) => {
    axios
      .post("https://localhost:5001/api/Member/Register", newMember)
      .then(function(res) {
        resolve(res.data);
      })
      .catch(function(err) {
        console.log(err);
        reject();
      });
  });
};

export const login = ({ commit }, loginInfo) => {
  return new Promise((resolve, reject) => {
    axios
      .post("https://localhost:5001/api/Member/Login", loginInfo)
      .then(function(res) {
        resolve(res.data);
        commit(types.SAVE_TOKEN, res.data);
      })
      .catch(function(err) {
        reject(err);
      });
  });
};

export const checkToken = ({ commit }, tokenInfo) => {
  commit(types.SAVE_TOKEN, tokenInfo);
};

export const logout = ({ commit }) => {
  commit(types.CLEAR_TOKEN);
};

export const updateProfile = ({ commit }, profileAfterEdit) => {
  return new Promise((resolve, reject) => {
    axios
      .post("https://localhost:5001/api/Member/EditMemberInformation", profileAfterEdit)
      .then(function(res) {
        resolve(res.data);
      })
      .catch(function(err) {
        console.log(err);
        reject();
      });
  });
};
