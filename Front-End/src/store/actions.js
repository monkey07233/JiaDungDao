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

export const memberRegistrationInformation = ({commit},newMember) => {
  axios
  .post("https://localhost:5001/api/Member/Register", newMember)
  .then(function(res) {
      if(res.data == 'successed'){
        commit(types.REGISTER_MESSAGE, res.data);
      }
      if(res.data == '帳號已存在'){
        commit(types.REGISTER_MESSAGE, res.data);
      }
  })
    .catch(function (err) {
      console.log(err);
    });
};
